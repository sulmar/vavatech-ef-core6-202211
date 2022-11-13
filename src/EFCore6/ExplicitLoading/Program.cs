
using Bogus;
using ExplicitLoading;
using ExplicitLoading.Models;


Console.WriteLine("Hello, Explicit Loading!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

var customers = GenerateCustomers(10);

if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(customers);  


    context.SaveChanges();
}



static Faker<Employee> GetEmployeeFaker() => new Faker<Employee>()
    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
    .RuleFor(p => p.LastName, f => f.Person.LastName);

static Faker<Bill> GetBillFaker() => new Faker<Bill>()
    .RuleFor(p => p.DateOfPayment, f => f.Date.Past());

static Faker<Order> GetOrderFaker() => new Faker<Order>()
    .RuleFor(p => p.TotalAmount, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(p=>p.Paid, f=>f.Random.Bool(0.7f))
    .RuleFor(p=>p.Bill, f=>GetBillFaker().Generate());
    
static IEnumerable<Customer> GenerateCustomers(int count) => new Faker<Customer>()
    .RuleFor(p => p.Name, f => f.Company.CompanyName())
    .RuleFor(p =>p.AccountManager, f => GetEmployeeFaker().Generate())
    .RuleFor(p=>p.Orders, f=> GetOrderFaker().GenerateBetween(1, 10))
    .Generate(count);