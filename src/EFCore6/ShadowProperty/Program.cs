using ShadowProperty;
using ShadowProperty.Models;

Console.WriteLine("Hello, Relationships!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

/// context.Database.EnsureDeleted();

if (context.Database.EnsureCreated())
{
    context.Products.AddRange(GenerateProducts());
    context.SaveChanges();
}

var product = context.Products.Find(1);
product.Price += 10;
context.Entry(product).Property("LastUpdated").CurrentValue = DateTime.Now;
context.SaveChanges();


Console.WriteLine(context.Entry(product).Property("LastUpdated").CurrentValue);

static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new Product("Product 1", 100),
    new Product("Product 2", 299),
    new Product("Product 3", 10),
};