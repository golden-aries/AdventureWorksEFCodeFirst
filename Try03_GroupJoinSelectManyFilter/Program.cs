using AdventureWorks;
using AdventureWorks.Entities.People;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json");
var conf = cb.Build();
var connString = conf.GetConnectionString(nameof(AdventureWorksContext));
using var db = new AdventureWorksContext(connString);
var q = db.BusinessEntities.GroupJoin(
                db.People,
                be => be.BusinessEntityID,
                p => p.BusinessEntityID,
                (be, p) => new { be, p })
        .SelectMany(
            beps => beps.p.DefaultIfEmpty(),
            (beps, p) => new { beps.be, p })
        .Where( r => r.p == null);

var result = await q.ToListAsync();

#region query
#if true
//SELECT 
//    [Extent1].[BusinessEntityID] AS [BusinessEntityID], 
//    [Extent1].[rowguid] AS [rowguid], 
//    [Extent1].[ModifiedDate] AS [ModifiedDate], 
//    [Extent2].[BusinessEntityID] AS [BusinessEntityID1], 
//    [Extent2].[PersonType] AS [PersonType], 
//    [Extent2].[NameStyle] AS [NameStyle], 
//    [Extent2].[Title] AS [Title], 
//    [Extent2].[FirstName] AS [FirstName], 
//    [Extent2].[MiddleName] AS [MiddleName], 
//    [Extent2].[LastName] AS [LastName], 
//    [Extent2].[Suffix] AS [Suffix], 
//    [Extent2].[EmailPromotion] AS [EmailPromotion], 
//    [Extent2].[AdditionalContactInfo] AS [AdditionalContactInfo], 
//    [Extent2].[Demographics] AS [Demographics], 
//    [Extent2].[rowguid] AS [rowguid1], 
//    [Extent2].[ModifiedDate] AS [ModifiedDate1]
//    FROM  [Person].[BusinessEntity] AS [Extent1]
//    LEFT OUTER JOIN [Person].[Person] AS [Extent2] ON [Extent1].[BusinessEntityID] = [Extent2].[BusinessEntityID]
//    WHERE [Extent2].[BusinessEntityID] IS NULL
#endif
#endregion

Console.WriteLine("Done!");
