using Migrations;
using Migrations.Models;

Console.WriteLine("Hello, Migrations!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Products.AddRange(GenerateProducts());

    context.SaveChanges();
}


static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new ("ZX Spectrum", 1000),
    new ("Atari 800 XL", 1000),
    new ("Commodore 64", 1000),
};