// See https://aka.ms/new-console-template for more information
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RawSql;
using RawSql.Models;
using System;
using System.Data;
using System.Data.Common;

Console.WriteLine("Hello, Raw SQL");


ContextFactory contextFactory = new ContextFactory();
var context = contextFactory.CreateDbContext(args);

// context.Database.EnsureDeleted();
if (context.Database.EnsureCreated())
{
    context.Orders.AddRange(GenerateOrders());

    context.SaveChanges();


    //context.Database.ExecuteSqlRaw(@"CREATE TABLE dbo.Posts
    //  (
    //    id int primary key,
    //    Title nvarchar(100),
    //    Content nvarchar(max),
    //    CreateDate datetime
    //  )");

    context.Database.ExecuteSqlRaw(@"CREATE VIEW PostsView AS SELECT TOP(10) * FROM Posts ORDER BY CreateDate desc");

    context.Set<Post>().AddRange(GeneratePosts());
    context.SaveChanges();

}


var posts = context.Set<Post>().ToList();


foreach (var post in posts)
{
    Console.WriteLine(post);
}


IEnumerable<Post> GeneratePosts() => new Faker<Post>()
        .UseSeed(0)
        .RuleFor(p => p.Title, f => f.Lorem.Sentence())
        .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
        .RuleFor(p => p.CreateDate, f => f.Date.Past())
        .Generate(50);


context.Database.ExecuteSqlRaw("UPDATE dbo.Customers SET Balance = Balance + 10");

// GET /api/customers/{id}
// GET /api/customers/10;DROP TABLE dbo.Customers
// GET /api/customers/10 OR 1=1

int customerId = 1;


// zła praktyka! uwaga na SQL Injection!
/*
var customer = context.Customers.FromSqlRaw(@"SELECT
    cust.Id,
    cust.Name,
    c.Id,
    c.Name
FROM Customers as cust
    INNER JOIN Countries as c ON cust.CountryId = c.Id
WHERE
    cust.Id = " + customerId);
*/


// Format string
var customer = context.Customers.FromSqlRaw(@"SELECT
    cust.Id,
    cust.Name,
    cust.Balance,
    cust.CountryId
FROM Customers as cust    
WHERE
    cust.Id = {0}", customerId).Include(p => p.Country).SingleOrDefault();


// String interpolation
var customer2 = context.Customers.FromSqlInterpolated($@"SELECT
    cust.Id,
    cust.Name,
    cust.Balance,
    cust.CountryId
FROM Customers as cust    
WHERE
    cust.Id = {customerId}").Include(p => p.Country).SingleOrDefault();



string sql = @"SELECT 
  	Countries.[Name] AS Country,
 	SUM(Orders.TotalAmount) AS TotalAmount
  FROM Orders 
	INNER JOIN Customers ON Orders.CustomerId = Customers.Id
	INNER JOIN Countries ON Customers.CountryId = Countries.Id
GROUP BY Countries.[Name]
";

var totalAmountCountries = context.TotalAmountCountries.FromSqlRaw(sql).ToList();

var query = context.TotalAmountCountries.ToList();

foreach (var totalAmountCountry in totalAmountCountries)
{
    Console.WriteLine(totalAmountCountry);
}


SqlQueryWithoutDbSet(context);

Console.WriteLine("Press Enter key to exit.");
Console.ReadLine();

IList<Country> GenerateCountries() => new List<Country>()
{
    new Country() { Name = "Poland"},
    new Country() { Name = "USA"},
    new Country() { Name = "France"},
};

IList<Customer> GenerateCustomers()
{
    var countries = GenerateCountries();

    var customers = new List<Customer>()
    {
        new Customer { Name = "Company 1", Country = countries[0] },
        new Customer { Name = "Company 2", Country = countries[1] },
        new Customer { Name = "Company 3", Country = countries[0] },
        new Customer { Name = "Company 4", Country = countries[1] },
        new Customer { Name = "Company 5", Country = countries[2] },
    };

    return customers;
}

ICollection<Order> GenerateOrders()
{
    var customers = GenerateCustomers();

    var orders = new List<Order>()
    {
        new Order { Customer = customers[0], OrderDate = DateTime.Now, TotalAmount = 1000 },
        new Order { Customer = customers[1], OrderDate = DateTime.Now, TotalAmount = 2000  },
        new Order { Customer = customers[2], OrderDate = DateTime.Now, TotalAmount = 3000  },
        new Order { Customer = customers[3], OrderDate = DateTime.Now, TotalAmount = 4000  },
    };

    return orders;
}

static void SqlQueryWithoutDbSet(ApplicationDbContext context)
{
    // Czasami pojawia się potrzeba wysłania zapytania SQL o dane, które nie są częścią encji. Na przykład w celu pobrania sekwencji.
    // Wówczas możemy skorzystać z ADO.NET i pobrać tylko połączenie z Context.Database.
    
    var results = new List<object>();

    using var command = context.Database.GetDbConnection().CreateCommand();
    command.CommandText = @"SELECT name, minimum_value, maximum_value from sys.sequences;";
    command.CommandType = CommandType.Text;
    context.Database.OpenConnection();

    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        results.Add(new
        {
            Name = (string)reader["name"],
            MinValue = (long)reader["minimum_value"],
            MaxValue = (long)reader["maximum_value"]
        });

    }
    context.Database.CloseConnection();
}
