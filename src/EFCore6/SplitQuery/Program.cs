using Bogus;
using Microsoft.EntityFrameworkCore;
using SplitQuery;
using SplitQuery.Models;

Console.WriteLine("Hello, Split Query!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

var customers = GenerateCustomers(10);
// var blogs = GenerateBlogs(5);

// context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(customers);


    context.SaveChanges();
}


// Włączenie lokalne rozdzielania zapytań
// var query = context.Customers.Include(c => c.Orders).AsSplitQuery().ToList();

// Wyłączenie rozdzielania zapytań
var singleQuery = context.Customers
    .Include(c => c.Orders)
    .AsSingleQuery()
    .ToList();

foreach (var customer in singleQuery)
{
    Console.WriteLine(customer.Name);
}


static IEnumerable<Customer> GenerateCustomers(int count) => new Faker<Customer>()
    .RuleFor(p => p.Name, f => f.Company.CompanyName())
    .RuleFor(p => p.AccountManager, f => GetEmployeeFaker().Generate())
    .RuleFor(p => p.Orders, f => GetOrderFaker().GenerateBetween(1, 10))
    .Generate(count);


static Faker<Bill> GetBillFaker() => new Faker<Bill>()
    .RuleFor(p => p.DateOfPayment, f => f.Date.Past());

static Faker<Order> GetOrderFaker() => new Faker<Order>()
    .RuleFor(p => p.TotalAmount, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(p => p.Paid, f => f.Random.Bool(0.7f))
    .RuleFor(p => p.Bill, f => GetBillFaker().Generate());

static Faker<Employee> GetEmployeeFaker() => new Faker<Employee>()
    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
    .RuleFor(p => p.LastName, f => f.Person.LastName)
;
