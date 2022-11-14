using CodeFirst;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, Code First!");

// dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0

//string connectionString = @"Server=(localdb)\mssqllocaldb;Database=CodeFirstDb";

//var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//    .UseSqlServer(connectionString)
//    .Options;

ApplicationDbContextFactory contextFactory = new ApplicationDbContextFactory();
using var context = contextFactory.CreateDbContext(args);

context.Database.EnsureDeleted();

var dbCreated = context.Database.EnsureCreated();

if (dbCreated)
{
    Console.WriteLine("Database created.");
}




// TODO: create a CustomersContext