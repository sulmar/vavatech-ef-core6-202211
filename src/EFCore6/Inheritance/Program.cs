using Inheritance;
using Inheritance.Models;

Console.WriteLine("Hello, Inheritance!");

MyContextFactory contextFactory1  = new MyContextFactory();
MyContext context1 = contextFactory1.CreateDbContext(args);
context1.Database.EnsureDeleted();
context1.Database.EnsureCreated();


var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Employees.AddRange(GenerateEmployees());
    /// context.Contractors.AddRange(GenerateEmployees().OfType<Contractor>());

    // context.Items.AddRange(GenerateItems());
    context.SaveChanges();
}



var query1 = context.Employees.ToList();

// TODO: Get fulltime employees only

var query = context.Employees.OfType<FullTimeEmployee>().ToList();

// TODO: Get products only

// TODO: Get services only


Console.WriteLine("Press any key to exit.");
Console.ReadKey();



static void Display<T>(IEnumerable<T> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static IEnumerable<Employee> GenerateEmployees() => new List<Employee>
{
    new FullTimeEmployee("John", "Smith"),
    new FullTimeEmployee("Kate", "Smith"),
    new Contractor("Mark", "Spider", DateTime.Parse("2022-11-01"), DateTime.Parse("2022-12-31"), 100),
};

static IEnumerable<Item> GenerateItems() => new List<Item>
{
    new Product("ZX Spectrum", 1000, "black", "L", 2f),
    new Product("Atari 800 XL", 1000, "brown", "L", 2f),
    new Product("Commodore 64", 1000, "white", "L", 2f),
    new Service("Cleaning", 100, TimeSpan.FromHours(3)),
    new Service("Installing", 100, TimeSpan.FromHours(1)),
};
