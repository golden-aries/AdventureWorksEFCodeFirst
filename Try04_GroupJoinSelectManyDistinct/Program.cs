using AdventureWorks;
using AdventureWorks.Entities.People;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json");
var conf = cb.Build();
var connString = conf.GetConnectionString(nameof(AdventureWorksContext));
using var db = new AdventureWorksContext(connString);

int i = 1580; // one of business entities
var q = db.ProductVendors.GroupJoin(
                db.BusinessEntities,
                pv => pv.Vendor.BusinessEntityID,
                be => be.BusinessEntityID,
                (pv, be) => new { pv, be })
        .SelectMany(
            pvbe => pvbe.be.DefaultIfEmpty(),
            (pvbe, be) => new {pvbe.pv, be })
        .Where(pvbe => pvbe.be.BusinessEntityID != i)
        .Select(bepv => bepv.pv.Product)
        .Distinct();

var result = await q.ToListAsync();

#region query
#if true
//SELECT 
//    [Distinct1].[ProductID] AS [ProductID], 
//    [Distinct1].[Name] AS [Name], 
//    [Distinct1].[ProductNumber] AS [ProductNumber], 
//    [Distinct1].[MakeFlag] AS [MakeFlag], 
//    [Distinct1].[FinishedGoodsFlag] AS [FinishedGoodsFlag], 
//    [Distinct1].[Color] AS [Color], 
//    [Distinct1].[SafetyStockLevel] AS [SafetyStockLevel], 
//    [Distinct1].[ReorderPoint] AS [ReorderPoint], 
//    [Distinct1].[StandardCost] AS [StandardCost], 
//    [Distinct1].[ListPrice] AS [ListPrice], 
//    [Distinct1].[Size] AS [Size], 
//    [Distinct1].[SizeUnitMeasureCode] AS [SizeUnitMeasureCode], 
//    [Distinct1].[WeightUnitMeasureCode] AS [WeightUnitMeasureCode], 
//    [Distinct1].[Weight] AS [Weight], 
//    [Distinct1].[DaysToManufacture] AS [DaysToManufacture], 
//    [Distinct1].[ProductLine] AS [ProductLine], 
//    [Distinct1].[Class] AS [Class], 
//    [Distinct1].[Style] AS [Style], 
//    [Distinct1].[ProductSubcategoryID] AS [ProductSubcategoryID], 
//    [Distinct1].[ProductModelID] AS [ProductModelID], 
//    [Distinct1].[SellStartDate] AS [SellStartDate], 
//    [Distinct1].[SellEndDate] AS [SellEndDate], 
//    [Distinct1].[DiscontinuedDate] AS [DiscontinuedDate], 
//    [Distinct1].[rowguid] AS [rowguid], 
//    [Distinct1].[ModifiedDate] AS [ModifiedDate]
//    FROM ( SELECT DISTINCT 
//        [Extent3].[ProductID] AS [ProductID], 
//        [Extent3].[Name] AS [Name], 
//        [Extent3].[ProductNumber] AS [ProductNumber], 
//        [Extent3].[MakeFlag] AS [MakeFlag], 
//        [Extent3].[FinishedGoodsFlag] AS [FinishedGoodsFlag], 
//        [Extent3].[Color] AS [Color], 
//        [Extent3].[SafetyStockLevel] AS [SafetyStockLevel], 
//        [Extent3].[ReorderPoint] AS [ReorderPoint], 
//        [Extent3].[StandardCost] AS [StandardCost], 
//        [Extent3].[ListPrice] AS [ListPrice], 
//        [Extent3].[Size] AS [Size], 
//        [Extent3].[SizeUnitMeasureCode] AS [SizeUnitMeasureCode], 
//        [Extent3].[WeightUnitMeasureCode] AS [WeightUnitMeasureCode], 
//        [Extent3].[Weight] AS [Weight], 
//        [Extent3].[DaysToManufacture] AS [DaysToManufacture], 
//        [Extent3].[ProductLine] AS [ProductLine], 
//        [Extent3].[Class] AS [Class], 
//        [Extent3].[Style] AS [Style], 
//        [Extent3].[ProductSubcategoryID] AS [ProductSubcategoryID], 
//        [Extent3].[ProductModelID] AS [ProductModelID], 
//        [Extent3].[SellStartDate] AS [SellStartDate], 
//        [Extent3].[SellEndDate] AS [SellEndDate], 
//        [Extent3].[DiscontinuedDate] AS [DiscontinuedDate], 
//        [Extent3].[rowguid] AS [rowguid], 
//        [Extent3].[ModifiedDate] AS [ModifiedDate]
//        FROM   [Purchasing].[ProductVendor] AS [Extent1]
//        LEFT OUTER JOIN [Person].[BusinessEntity] AS [Extent2] ON [Extent1].[BusinessEntityID] = [Extent2].[BusinessEntityID]
//        INNER JOIN [Production].[Product] AS [Extent3] ON [Extent1].[ProductID] = [Extent3].[ProductID]
//        WHERE  NOT (([Extent2].[BusinessEntityID] = @p__linq__0) AND ((CASE WHEN ([Extent2].[BusinessEntityID] IS NULL) THEN cast(1 as bit) ELSE cast(0 as bit) END) = 0))
//    )  AS [Distinct1]

#endif
#endregion

Console.WriteLine("Done!");
