
using ComputedColumnSql;
using ComputedColumnSql.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello,  Computed Column Sql!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    string sql = @"
        CREATE TRIGGER [dbo].[Customer_UPDATE] ON [dbo].[Customers]
                AFTER UPDATE
            AS
            BEGIN
                SET NOCOUNT ON;

                IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                DECLARE @Id INT

                SELECT @Id = INSERTED.Id
                FROM INSERTED

                UPDATE dbo.Customers
                SET ModifiedOn = GETUTCDATE()
                WHERE Id = @Id
            END
";

    context.Database.ExecuteSqlRaw(sql);

    context.Customers.AddRange(GenerateCustomers());
    context.Invoices.AddRange(GenerateInvoices());
    context.SaveChanges();
}


var customer = context.Customers.SingleOrDefault(c => c.FullName == "John Smith");

Console.WriteLine(customer);

customer.Balance += 100;

context.SaveChanges();

Console.WriteLine(customer.ModifiedOn);


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