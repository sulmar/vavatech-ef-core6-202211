
using ViewMapping;
using ViewMapping.Models;

Console.WriteLine("Hello, View Mapping!");

var contextFactory = new ApplicationDbContextFactory();
var crmContextFactory = new CrmDbContextFactory();
using var context = contextFactory.CreateDbContext(args);
using var crmContext = crmContextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.ApplicationUsers.AddRange(GenerateApplicationUsers());

    context.SaveChanges();
}

var applicationUsers = context.ApplicationUsers.ToList();

var crmUsers = crmContext.ApplicationUsers.ToList();

Console.WriteLine("Press any key to exit.");
Console.ReadLine();

static IEnumerable<ApplicationUser> GenerateApplicationUsers()
{
    var users = new List<ApplicationUser>
        {
            new ApplicationUser { AccountNumber = "1111", Username = "john", Password = "123" },
            new ApplicationUser { AccountNumber = "2222", Username = "kate", Password = "123" }
        };

    return users;
}