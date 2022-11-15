using Microsoft.EntityFrameworkCore;
using TemporalTables;
using TemporalTables.Models;

Console.WriteLine("Hello, Temporal Tables!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

// context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Products.AddRange(GenerateProducts());
    context.SaveChanges();
}


// TODO: Get current price
var currentPrice = context.Products.Find(1).Price;

Console.WriteLine(currentPrice);


// TODO: Change price
var product = context.Products.Find(1);

product.Price += 10;

context.SaveChanges();


// TODO: Get historical prices
var historical = context.Products.TemporalAll()
    .Where(p => p.Name == "DeLorean")
    .Select(p => new
    {
        Product = p,
        PeriodStart = EF.Property<DateTime>(p, "PeriodStart"),
        PeriodEnd = EF.Property<DateTime>(p, "PeriodEnd"),
    })
    .ToList();

foreach (var item in historical)
{
    Console.WriteLine(item);
}

// TODO: Get price on date

var selectedDate = DateTime.Parse("2022-11-15 13:10");

var productOnDate = context.Products.TemporalAsOf(selectedDate).Single(p=>p.Name == "DeLorean");

Console.WriteLine(productOnDate);


Console.WriteLine("Press any key to exit.");
Console.ReadKey();




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
