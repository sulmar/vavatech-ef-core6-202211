
using Conventions;
using Conventions.Models;

Console.WriteLine("Hello, Conventions!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.SaveChanges();
}

var customers = context.Customers.ToList();

Display(customers);


static void Display(IEnumerable<Customer> customers)
{
    foreach (var customer in customers)
    {
        Console.WriteLine(customer);
    }
}
static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new Customer("John", "Smith"),  
    new Customer("Kate", "Smith"),
    new Customer("Jack", "London"),
};