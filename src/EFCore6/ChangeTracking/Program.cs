
using ChangeTracking;
using ChangeTracking.Models;

Console.WriteLine("Hello, Change Tracking!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.Products.AddRange(GenerateProducts());

    context.SaveChanges();
}

static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new ("John", "Smith"),
    new ("Kate", "Spider"),    
};


static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new Product("ZX Spectrum", 1000),
    new Product("Atari 800 XL", 1000),
    new Product("Commodore 64", 1000),    
};