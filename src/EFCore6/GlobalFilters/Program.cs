using GlobalFilters;
using GlobalFilters.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, Global Filters!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.SaveChanges();
}


// TODO: Get number of active customers

// var customers = context.Customers.Where(c=>c.IsRemoved==false).ToList();


var query = context.Customers.ToList();

var customers = context.Customers.Where(c => c.Age > 18).ToList();

// Wyłączenie filtru
var query2 = context.Customers.IgnoreQueryFilters().ToList();

foreach (var customer in customers)
{
    Console.WriteLine(customer);
}

// TODO: Calculate average age of active customers

// TODO: Get number off all customers

// TODO: Calculate average age of active customers for specified tenant


static void Display<T>(IEnumerable<T> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new("John", "Smith", 50, 1, false),
    new("Kate", "Smith", 40, 1, false),
    new("Mark", "Spider",30, 2, false),
    new("Ann", "Spider", 20, 2, true),

};
