using Bogus;
using Indexes;
using Indexes.Models;

Console.WriteLine("Hello, Indexes!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Products.AddRange(GenerateProducts(100_000));
    context.SaveChanges();
}


// TODO: filter by color

// TODO: filter by on stock

// TODO: filter by discontinued



static IEnumerable<Product> GenerateProducts(int count) => new Faker<Product>()
    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
    .RuleFor(p => p.Color, f => f.Commerce.Color())
    .RuleFor(p => p.OnStock, f => f.Random.Bool(0.7f))
    .RuleFor(p => p.Discontinued, f => f.Random.Bool(0.1f))
    .Generate(count);