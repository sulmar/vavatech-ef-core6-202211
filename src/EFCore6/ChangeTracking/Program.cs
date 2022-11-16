
using ChangeTracking;
using ChangeTracking.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, Change Tracking!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

// context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.Products.AddRange(GenerateProducts());

    context.SaveChanges();
}

var order = GenerateOrder();

var json = System.Text.Json.JsonSerializer.Serialize(order);

// POST api/orders

var newOrder = System.Text.Json.JsonSerializer.Deserialize<Order>(json);

// AsNoTracking(context);



var products = context.Products.ToList();

var product = products.First();
Console.WriteLine(context.Entry(product).State);

// product.Price += 100;
product.Name = product.Name + "!";
Console.WriteLine(context.Entry(product).State);

context.SaveChanges();
Console.WriteLine(context.Entry(product).State);



// Automatyczne sterowanie za pomocą TrackGraph
// TrackGraph(context, newOrder);

// DisableChangeTracking(context);

// context.Orders.Add(newOrder);

// AddComplexDetachedEntity(context, newOrder);



// var product1 = context.Products.SingleOrDefault(p => p.Name == "Commodore 64");

// Odłączona encja 
// DetachEntity(context);

// ManualChangeEntityState(context);

// RemoveEntity(context);
// UpdateEntity(context);

// Modyfikacja

// Update(context);
// Add(context);
// Remove(context);

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

static IEnumerable<Customer> GenerateCustomers() => new List<Customer>
{
    new ("John", "Smith"),
    new ("Kate", "Spider"),
};


static IEnumerable<Product> GenerateProducts() => new List<Product>
{
    new Product("ZX Spectrum", 1000),
    new Product("Atari 800 XL", 1000),
    new Product("Commodore 64", 1000),
};

static void Update(ApplicationDbContext context)
{
    var product = context.Products.Find(1);

    Console.WriteLine(context.Entry(product).State);

    var priceProperty = context.Entry(product).Property(p => p.Price);
    var nameProperty = context.Entry(product).Property(p => p.Name);

    Console.WriteLine($" {priceProperty.Metadata.Name} {priceProperty.IsModified}");
    Console.WriteLine($" {nameProperty.Metadata.Name} {nameProperty.IsModified}");


    product.Price += 10;

    Console.WriteLine(context.Entry(product).State);
    Console.WriteLine($" {priceProperty.Metadata.Name} {priceProperty.IsModified}");
    Console.WriteLine($" {nameProperty.Metadata.Name} {nameProperty.IsModified}");


    context.SaveChanges();

    Console.WriteLine(context.Entry(product).State);
    Console.WriteLine($" {priceProperty.Metadata.Name} {priceProperty.IsModified}");
    Console.WriteLine($" {nameProperty.Metadata.Name} {nameProperty.IsModified}");

    product.Name = product.Name + "!";

    Console.WriteLine(context.Entry(product).State);
    Console.WriteLine($" {priceProperty.Metadata.Name} {priceProperty.IsModified}");
    Console.WriteLine($" {nameProperty.Metadata.Name} {nameProperty.IsModified}");


    context.SaveChanges();

    Console.WriteLine(context.Entry(product).State);
    Console.WriteLine($" {nameProperty.Metadata.Name} {nameProperty.IsModified}");
}

static void Add(ApplicationDbContext context)
{
    var product = new Product("PC 386SX", 2000);
    Console.WriteLine(context.Entry(product).State);

    context.Products.Add(product);
    Console.WriteLine(context.Entry(product).State);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product).State);
}

static void Remove(ApplicationDbContext context)
{
    var product = context.Products.First();
    Console.WriteLine(context.Entry(product).State);

    context.Products.Remove(product);
    Console.WriteLine(context.Entry(product).State);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product).State);
}

static void DetachEntity(ApplicationDbContext context)
{
    var product1 = new Product("Commode 64", 200) { Id = 3 };
    Console.WriteLine(context.Entry(product1).State);

    context.Entry(product1).Property(p => p.Price).IsModified = true;
    context.Entry(product1).Property(p => p.Name).IsModified = true;

    // context.Entry(product1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    Console.WriteLine(context.Entry(product1).State);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product1).State);
}

// DELETE api/products/{id}
static void RemoveEntity(ApplicationDbContext context)
{
    int id = 3;

    // var product = context.Products.Find(id);
    var fakeProduct = new Product { Id = id };
    context.Products.Remove(fakeProduct);
    context.SaveChanges();
}

// PUT api/products/{id}

static void UpdateEntity(ApplicationDbContext context)
{
    int id = 4;

    // var product = context.Products.Find(id);
    var fakeProduct = new Product { Id = id, Name = "Amiga", Price = 3000 };

    Console.WriteLine(context.Entry(fakeProduct).State);
    context.Products.Update(fakeProduct);
    Console.WriteLine(context.Entry(fakeProduct).State);
    context.SaveChanges();
    Console.WriteLine(context.Entry(fakeProduct).State);
}

static void ManualChangeEntityState(ApplicationDbContext context)
{
    var product = context.Products.First();
    Console.WriteLine(context.Entry(product).State);

    product.Price = 0;
    Console.WriteLine(context.Entry(product).State);

    context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
    Console.WriteLine(context.Entry(product).State);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product).State);
}

static void AddComplexDetachedEntity(ApplicationDbContext context, Order? newOrder)
{
    Console.WriteLine(context.Entry(newOrder).State);

    context.Entry(newOrder.Customer).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

    context.Orders.Add(newOrder);
    Console.WriteLine(context.Entry(newOrder).State);
    Console.WriteLine(context.Entry(newOrder.Customer).State);


    foreach (var detail in newOrder.Details)
    {
        context.Entry(detail.Product).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
    }

    var entries = context.ChangeTracker.Entries();

    foreach (var entry in entries)
    {
        Console.WriteLine($"{entry.Entity} {entry.State}");
    }

    Console.WriteLine(context.ChangeTracker.DebugView.ShortView);

    Console.WriteLine(context.ChangeTracker.DebugView.LongView);

    context.SaveChanges();
    Console.WriteLine(context.Entry(newOrder).State);
}

static Order GenerateOrder() => new Order
{
    Number = "ZAM 1",
    Customer = new Customer("John", "Smith") { Id = 1 },
    Details = new List<OrderDetail>
    {
        new OrderDetail { Product = new Product { Id = 3, Name ="Commodore 64", Price = 1000 }, Quantity = 3, Amount = 1000 },
        // new OrderDetail { Product = new Product { Id = 4, Name ="Amiga", Price = 3000 }, Quantity = 1, Amount  = 2500 },
    }
};

static void TrackGraph(ApplicationDbContext context, Order newOrder)
{
    context.ChangeTracker.TrackGraph(newOrder, node =>
    {
        Console.WriteLine($"{node.Entry.Metadata.Name} {node.Entry.State}");

        if (node.Entry.IsKeySet)
        {
            node.Entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
        }
        else
        {
            node.Entry.State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
    });

    Console.WriteLine(context.ChangeTracker.DebugView.ShortView);

    context.SaveChanges();
}

static void DisableChangeTracking(ApplicationDbContext context)
{
    context.ChangeTracker.AutoDetectChangesEnabled = false;

    var product = new Product { Id = 1, Name = "ZX80", Price = 2000 };
    Console.WriteLine(context.Entry(product).State);

    context.Products.Attach(product);
    Console.WriteLine(context.Entry(product).State);

    product.Price += 10;
    Console.WriteLine(context.Entry(product).State);


    var priceProperty = context.Entry(product).Property(p => p.Price);
    var nameProperty = context.Entry(product).Property(p => p.Name);

    Console.WriteLine($"OriginalValue: {priceProperty.OriginalValue} CurrentValue: {priceProperty.CurrentValue}");
    Console.WriteLine($"OriginalValue: {nameProperty.OriginalValue} CurrentValue: {nameProperty.CurrentValue}");

    context.ChangeTracker.DetectChanges();
    Console.WriteLine(context.Entry(product).State);
    Console.WriteLine($"OriginalValue: {priceProperty.OriginalValue} CurrentValue: {priceProperty.CurrentValue}");
    Console.WriteLine($"OriginalValue: {nameProperty.OriginalValue} CurrentValue: {nameProperty.CurrentValue}");

    Console.WriteLine(context.ChangeTracker.DebugView.LongView);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product).State);
}

static void AsNoTracking(ApplicationDbContext context)
{
    // Lokalne wyłączenie śledzenia zmian
    // var products = context.Products.AsNoTracking().ToList();

    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    var products = context.Products.ToList();

    var product = products.First();

    Console.WriteLine(context.Entry(product).State);

    product.Price += 100;
    Console.WriteLine(context.Entry(product).State);

    context.SaveChanges();
    Console.WriteLine(context.Entry(product).State);
}