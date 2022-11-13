using TemporalTables;
using TemporalTables.Models;

Console.WriteLine("Hello, Temporal Tables!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Products.AddRange(GenerateProducts());
    context.SaveChanges();
}


// TODO: Change price

// TODO: Get current price

// TODO: Get historical prices

// TODO: Get price on date




static void Display(IEnumerable<Product> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new("DeLorean", 1_000_000m, "Silver"),
    new("Landrover Defender", 500_000m, "Green"),
    new("Tesla S", 250_000m, "Black"),
};
