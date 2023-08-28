using AdventureWorks;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json");
var conf = cb.Build();
var connString = conf.GetConnectionString(nameof(AdventureWorksContext));
using var db = new AdventureWorksContext(connString);
var result = await db.People.Take(10).ToListAsync();

Console.WriteLine("Done!");
