// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Microsoft.EntityFrameworkCore.Cosmos.Query.Expressions.Internal
{
    public class RootReferenceExpression : Expression, IPrintable
    {
        private readonly IEntityType _entityType;
        private readonly string _alias;

        public override ExpressionType NodeType => ExpressionType.Extension;
        public override Type Type => _entityType.ClrType;

        public RootReferenceExpression(IEntityType entityType, string alias)
        {
            _entityType = entityType;
            _alias = alias;
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor) => this;

        public override string ToString() => _alias;

        public void Print(ExpressionPrinter expressionPrinter)
        {
            throw new NotImplementedException();
        }
    }
}
