// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Cosmos.Query.Translators.Internal
{
    public interface IMemberTranslator
    {
        SqlExpression Translate(SqlExpression instance, MemberInfo member, Type returnType);
    }
}
