// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Cosmos.Query.ExpressionVisitors.Internal;

namespace Microsoft.EntityFrameworkCore.Cosmos.Query.Internal
{
    public class QuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {
        public QuerySqlGenerator Create()
        {
            return new QuerySqlGenerator();
        }
    }
}
