
using ComputedColumnSql;
using ComputedColumnSql.Models;

Console.WriteLine("Hello,  Computed Column Sql!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.Invoices.AddRange(GenerateInvoices());
    context.SaveChanges();
}


// TODO: Change price

// TODO: Get current price

// TODO: Get historical prices

// TODO: Get price on date




static void Display(IEnumerable<Customer> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new("John", "Smith"),
    new("Kate", "Smith"),
    new("Mark", "Spider"),
};

static IEnumerable<Invoice> GenerateInvoices() => new List<Invoice>
{
    new (100, 0.23m),
    new (100, 0.07m),
    new (100, 0.00m),
};