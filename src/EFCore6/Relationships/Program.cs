using Microsoft.EntityFrameworkCore;
using Relationships;

Console.WriteLine("Hello, Relationships!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.EnsureDeleted();

if (context.Database.EnsureCreated())
{
    // TODO: generate data


    var query1 = context.Orders.Where(o => o.Customer.Id == 1)
        .TagWith("Navigation Property")
        .ToList();


    var query2 = context.Orders.Where(o => o.CustomerId == 1)
        .TagWith("Shadow Property")
        .ToList();


    context.SaveChanges();
}





