using Relationships;

Console.WriteLine("Hello, Relationships!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.EnsureDeleted();

if (context.Database.EnsureCreated())
{
    // TODO: generate data
   
    context.SaveChanges();
}





