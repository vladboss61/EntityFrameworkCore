// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Microsoft.EntityFrameworkCore.Query.Pipeline
{
    public class CompiledQuery<TContext, TSyncResult, TAsyncResult>
        where TContext : DbContext
    {
        private readonly LambdaExpression _queryExpression;
        private Func<QueryContext, TSyncResult> _syncExecutor;
        private Func<QueryContext, TAsyncResult> _asyncExecutor;

        public CompiledQuery(LambdaExpression queryExpression)
        {
            _queryExpression = queryExpression;
        }

        public TSyncResult Execute(TContext context, params object[] parameters)
        {
            var executor = EnsureSyncExecutor(context);
            var queryContextFactory = context.GetService<IQueryContextFactory>();
            var queryContext = queryContextFactory.Create();

            for (var i = 0; i < parameters.Length; i++)
            {
                queryContext.AddParameter(
                    CompiledQueryCache.CompiledQueryParameterPrefix + _queryExpression.Parameters[i + 1].Name,
                    parameters[i]);
            }

            return executor(queryContext);
        }

        public TAsyncResult ExecuteAsync(TContext context, CancellationToken cancellationToken = default, params object[] parameters)
        {
            var executor = EnsureAsyncExecutor(context);
            var queryContextFactory = context.GetService<IQueryContextFactory>();
            var queryContext = queryContextFactory.Create();

            queryContext.CancellationToken = cancellationToken;

            for (var i = 0; i < parameters.Length; i++)
            {
                queryContext.AddParameter(
                    CompiledQueryCache.CompiledQueryParameterPrefix + _queryExpression.Parameters[i + 1].Name,
                    parameters[i]);
            }

            return executor(queryContext);
        }

        private Func<QueryContext, TSyncResult> EnsureSyncExecutor(TContext context)
            => NonCapturingLazyInitializer.EnsureInitialized(
                ref _syncExecutor,
                context,
                _queryExpression,
                (c, q) =>
                {
                    var queryCompiler = context.GetService<IQueryCompiler>();
                    var expression = new QueryExpressionRewriter(c, q.Parameters).Visit(q.Body);

                    return queryCompiler.CreateCompiledQuery<TSyncResult>(expression);
                });

        private Func<QueryContext, TAsyncResult> EnsureAsyncExecutor(TContext context)
            => NonCapturingLazyInitializer.EnsureInitialized(
                ref _asyncExecutor,
                context,
                _queryExpression,
                (c, q) =>
                {
                    var queryCompiler = context.GetService<IQueryCompiler>();
                    var expression = new QueryExpressionRewriter(c, q.Parameters).Visit(q.Body);

                    return queryCompiler.CreateCompiledAsyncQuery<TAsyncResult>(expression);
                });

        private sealed class QueryExpressionRewriter : ExpressionVisitor
        {
            private readonly TContext _context;
            private readonly IReadOnlyCollection<ParameterExpression> _parameters;

            public QueryExpressionRewriter(
                TContext context, IReadOnlyCollection<ParameterExpression> parameters)
            {
                _context = context;
                _parameters = parameters;
            }

            protected override Expression VisitParameter(ParameterExpression parameterExpression)
            {
                if (typeof(TContext).GetTypeInfo().IsAssignableFrom(parameterExpression.Type.GetTypeInfo()))
                {
                    return Expression.Constant(_context);
                }

                return _parameters.Contains(parameterExpression)
                    ? Expression.Parameter(
                        parameterExpression.Type,
                        CompiledQueryCache.CompiledQueryParameterPrefix + parameterExpression.Name)
                    : parameterExpression;
            }
        }
    }
}
