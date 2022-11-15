using Microsoft.EntityFrameworkCore;
using Migrations;
using Migrations.Models;

Console.WriteLine("Hello, Migrations!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.Migrate();

if (!context.Products.Any())
{
    context.Products.AddRange(GenerateProducts());

    context.SaveChanges();
}


var products = context.Products.Select(p => new { p.Id, p.Name, p.Price, p.Description, p.Color }).ToList();

// dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.0


foreach (var product in products)
{
    Console.WriteLine(product);
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

// od EF Core 6
// dotnet ef migrations bundle
// efbundle.exe --connection "Data Source=(localdb)\mssqllocaldb;Initial Catalog=BundleMigrationsDb"


static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new ("ZX Spectrum", 1000)  { Description = "Lorem ipsum"},
    new ("Atari 800 XL", 1000) { Description = "Lorem ipsum"},
    new ("Commodore 64", 1000) { Description = "Lorem ipsum"},
};