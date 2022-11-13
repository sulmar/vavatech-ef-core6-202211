using Inheritance;
using Inheritance.Models;

Console.WriteLine("Hello, Inheritance!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Employees.AddRange(GenerateEmployees());
    context.Items.AddRange(GenerateItems());
    context.SaveChanges();
}



// TODO: Get fulltime employees only

// TODO: Get products only

// TODO: Get services only




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
