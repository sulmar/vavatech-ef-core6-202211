
using Conversions;
using Conversions.Models;

Console.WriteLine("Hello, Conversions!");

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
    new Customer
    {
        FirstName = "John", LastName = "Smith", 
        MembershipType = MembershipType.Free,
        Location = new Coordinate(52.01, 21.05),
        Profile = new Profile { Theme = "Dark", Volume = 0.5f },
        DateOfBirth = new DateOnly(1990, 5, 1),
        WakeupHour = new TimeOnly(7, 30),
        CanSend = true
    },

    new Customer
    {
        FirstName = "Kate", LastName = "Smith", 
        MembershipType = MembershipType.Standard,
        Location = new Coordinate(52.01, 21.05),
        Profile = new Profile { Theme = "Light", Volume = 1.0f },
        DateOfBirth = new DateOnly(1998, 7, 1),
        WakeupHour = new TimeOnly(6, 30),
    },

    new Customer
    {
        FirstName = "Jack", LastName = "London", 
        MembershipType = MembershipType.Premium,
        Location = new Coordinate(51.21, 20.65),
        Profile = new Profile { Theme = "Light", Volume = 0.1f },
        DateOfBirth = new DateOnly(1980, 1, 20),
        WakeupHour = new TimeOnly(6, 0)
    }
};