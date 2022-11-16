
using ConcurrencyManagement;
using ConcurrencyManagement.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello,  Concurrency Management!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

// context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.SaveChanges();
}

// TODO: Change John's balance

int customerId = 1;

// 1. user1
using var user1Context = contextFactory.CreateDbContext(args);
var customerUser1 = user1Context.Customers.Find(customerId);
customerUser1.Balance += 100;

// 2. user2
using var user2Context = contextFactory.CreateDbContext(args);
var customerUser2 = user2Context.Customers.Find(customerId);
customerUser2.Balance -= 100;
user2Context.SaveChanges();

await Task.Delay(3000);

try
{
    user1Context.SaveChanges();
}
catch(DbUpdateConcurrencyException e)
{
    Console.WriteLine("Dane zostały w międzyczasie zmodyfikowane. Zapis anulowany.");

    foreach(var entry in e.Entries)
    {
        var customer = entry.Entity as Customer;

        Console.WriteLine(customer.FirstName);

        Console.WriteLine(customer.Balance);

        var currentBalance = customer.Balance;

        Console.WriteLine("Reload");
        entry.Reload();

        Console.WriteLine(customer.Balance);

        Console.WriteLine("Czy nadpisać? (T)ak, (N)ie ");

        if (Console.ReadLine() == "T")
        {
            customer.Balance = currentBalance;
            context.SaveChanges();
        }
    }
}


// TODO: Change Ann's lastname




static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new Customer { FirstName = "John", LastName = "Smith", Balance = 1000 },
    new Customer { FirstName = "Ann", LastName = "Spider", Balance = 1000 }
};