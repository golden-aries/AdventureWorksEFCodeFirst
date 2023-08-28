using AdventureWorks;
using AdventureWorks.Entities.People;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json");
var conf = cb.Build();
var connString = conf.GetConnectionString(nameof(AdventureWorksContext));
using var db = new AdventureWorksContext(connString);
var result = await db.People.Take(10).ToListAsync();

#region data test
/*
 It looks like business entity is either of three: Person, Vendor or Store
 SELECT 
	be.BusinessEntityID,
	case
		when p.BusinessEntityID is not null then 'Person'
		when v.BusinessEntityID is not null then 'Vendor'
		when s.BusinessEntityID is not null then 'Store'
	end AS business_entity_type,
	*  
 FROM Person.BusinessEntity  AS be
	LEFT OUTER JOIN Person.Person AS  p
		ON be.BusinessEntityID = p.BusinessEntityID
	LEFT OUTER JOIN Purchasing.Vendor AS v
		ON v.BusinessEntityID = be.BusinessEntityID
	LEFT OUTER JOIN Sales.Store AS s
		ON s.BusinessEntityID = be.BusinessEntityID
-- WHERE p.BusinessEntityID is null
--WHERE 
--	(p.BusinessEntityID + v.BusinessEntityID is not null)
--	OR
--    (v.BusinessEntityID + s.BusinessEntityID is not null)
--	OR
--	(p.BusinessEntityID + s.BusinessEntityID is not null)
-- result 0
WHERE 
	p.BusinessEntityID is null
	AND
    v.BusinessEntityID is null
	AND
	s.BusinessEntityID is null
-- result 0
 */
#endregion

Console.WriteLine("Done!");
