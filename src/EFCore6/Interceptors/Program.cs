
using Bogus;
using Interceptors;
using Interceptors.Models;

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    var customers = new Faker<Customer>()
        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
        .RuleFor(p => p.LastName, f => f.Person.LastName)
        .Generate(100);

    var products = new Faker<Product>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
        .Generate(100);

    context.Customers.AddRange(customers);
    context.Products.AddRange(products);
    context.SaveChanges();
}

var customer = context.Customers.First();

customer.FirstName = "Anna";

context.SaveChanges();
