// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class CompiledQuerySqlServerTest : CompiledQueryTestBase<NorthwindQuerySqlServerFixture<NoopModelCustomizer>>
    {
        public CompiledQuerySqlServerTest(NorthwindQuerySqlServerFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
            //fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public override async Task DbSet_query(bool isAsync)
        {
            await base.DbSet_query(isAsync);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        public override async Task DbSet_query_first(bool isAsync)
        {
            await base.DbSet_query_first(isAsync);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Query_ending_with_include(bool isAsync)
        {
            await base.Query_ending_with_include(isAsync);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
ORDER BY [c].[CustomerID], [o].[OrderID]");
        }

        public override async Task Untyped_context(bool isAsync)
        {
            await base.Untyped_context(isAsync);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        //        public override async Task Query_with_single_parameter(bool isAsync)
        //        {
        //            await base.Query_with_single_parameter(isAsync);

        //            AssertSql(
        //                @"@__customerID='ALFKI' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID",
        //                //
        //                @"@__customerID='ANATR' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID");
        //        }

        //        public override async Task First_query_with_single_parameter(bool isAsync)
        //        {
        //            await base.First_query_with_single_parameter(isAsync);

        //            AssertSql(
        //                @"@__customerID='ALFKI' (Size = 5)

        //SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID",
        //                //
        //                @"@__customerID='ANATR' (Size = 5)

        //SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID");
        //        }

        //        public override async Task Query_with_two_parameters(bool isAsync)
        //        {
        //            await base.Query_with_two_parameters(isAsync);

        //            AssertSql(
        //                @"@__customerID='ALFKI' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID",
        //                //
        //                @"@__customerID='ANATR' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID");
        //        }

        //        public override async Task Query_with_three_parameters(bool isAsync)
        //        {
        //            await base.Query_with_three_parameters(isAsync);

        //            AssertSql(
        //                @"@__customerID='ALFKI' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID",
        //                //
        //                @"@__customerID='ANATR' (Size = 5)

        //SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = @__customerID");
        //        }

        //        public override async Task Query_with_array_parameter(bool isAsync)
        //        {
        //            await base.Query_with_array_parameter(isAsync);

        //            AssertSql(
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]",
        //                //
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]");
        //        }

        //        public override async Task Query_with_contains(bool isAsync)
        //        {
        //            await base.Query_with_contains(isAsync);

        //            AssertSql(
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] IN (N'ALFKI')",
        //                //
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] IN (N'ANATR')");
        //        }

        //        public override async Task Query_with_closure(bool isAsync)
        //        {
        //            await base.Query_with_closure(isAsync);

        //            AssertSql(
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = N'ALFKI'",
        //                //
        //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
        //FROM [Customers] AS [c]
        //WHERE [c].[CustomerID] = N'ALFKI'");
        //        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}
