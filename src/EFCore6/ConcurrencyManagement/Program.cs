
using ConcurrencyManagement;
using ConcurrencyManagement.Models;

Console.WriteLine("Hello,  Concurrency Management!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.SaveChanges();
}

// TODO: Change John's balance


// TODO: Change Ann's lastname




static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new Customer { FirstName = "John", LastName = "Smith", Balance = 1000 },
    new Customer { FirstName = "Ann", LastName = "Spider", Balance = 1000 }
};