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
                (be, p) => new { be, p });

var result = await q.ToListAsync();

#region Query
/*
SELECT
        [Project1].[BusinessEntityID] AS[BusinessEntityID], 
        [Project1].[rowguid] AS[rowguid], 
        [Project1].[ModifiedDate] AS[ModifiedDate], 
        [Project1].[C1] AS[C1], 
        [Project1].[BusinessEntityID1] AS[BusinessEntityID1], 
        [Project1].[PersonType] AS[PersonType], 
        [Project1].[NameStyle] AS[NameStyle], 
        [Project1].[Title] AS[Title], 
        [Project1].[FirstName] AS[FirstName], 
        [Project1].[MiddleName] AS[MiddleName], 
        [Project1].[LastName] AS[LastName], 
        [Project1].[Suffix] AS[Suffix], 
        [Project1].[EmailPromotion] AS[EmailPromotion], 
        [Project1].[AdditionalContactInfo] AS[AdditionalContactInfo], 
        [Project1].[Demographics] AS[Demographics], 
        [Project1].[rowguid1] AS[rowguid1], 
        [Project1].[ModifiedDate1] AS[ModifiedDate1]
    FROM(
        SELECT
                [Extent1].[BusinessEntityID] AS[BusinessEntityID],
                [Extent1].[rowguid] AS[rowguid],
                [Extent1].[ModifiedDate] AS[ModifiedDate],
                [Extent2].[BusinessEntityID] AS[BusinessEntityID1],
                [Extent2].[PersonType] AS[PersonType],
                [Extent2].[NameStyle] AS[NameStyle],
                [Extent2].[Title] AS[Title],
                [Extent2].[FirstName] AS[FirstName],
                [Extent2].[MiddleName] AS[MiddleName],
                [Extent2].[LastName] AS[LastName],
                [Extent2].[Suffix] AS[Suffix],
                [Extent2].[EmailPromotion] AS[EmailPromotion],
                [Extent2].[AdditionalContactInfo] AS[AdditionalContactInfo],
                [Extent2].[Demographics] AS[Demographics],
                [Extent2].[rowguid] AS[rowguid1],
                [Extent2].[ModifiedDate] AS[ModifiedDate1],
                CASE WHEN([Extent2].[BusinessEntityID] IS NULL) THEN CAST(NULL AS int) ELSE 1 END AS[C1]
            FROM [Person].[BusinessEntity] AS [Extent1]
            LEFT OUTER JOIN [Person].[Person] AS [Extent2] 
                ON [Extent1].[BusinessEntityID] = [Extent2].[BusinessEntityID]
    )  AS [Project1]
    ORDER BY [Project1].[BusinessEntityID] ASC, [Project1].[C1] ASC
*/
#endregion

Console.WriteLine("Done!");
