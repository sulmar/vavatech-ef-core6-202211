using DependencyInjection;
using DependencyInjection.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContextPool<CRMDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();


//using(var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); 

//    context.Database.EnsureCreated();
//}

// Refaktoring
// CreateDatabase<ApplicationDbContext>(app);

// Za pomoc¹ metody rozszerzaj¹cej (Extension Method)
app.CreateDatabase<ApplicationDbContext>();
app.CreateDatabase<CRMDbContext>();

SeedData(app);

// Configure the HTTP request pipeline.

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/products", (ApplicationDbContext context) => context.Products.ToList());

app.MapGet("/api/users", (CRMDbContext context) => context.ApplicationUsers.ToList());

app.Run();

//static void CreateDatabase<TContext>(WebApplication app)
//    where TContext : DbContext
//{
//    using var scope = app.Services.CreateScope();

//    var context = scope.ServiceProvider.GetRequiredService<TContext>();

//    context.Database.EnsureCreated();
//}

static void SeedData(WebApplication app)    
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.ApplicationUsers.Any())
    {
        var users = new List<ApplicationUser>
        {
            new ApplicationUser { AccountNumber = "1111", Username = "john", Password = "123" },
            new ApplicationUser { AccountNumber = "2222", Username = "kate", Password = "123" }
        };

        context.ApplicationUsers.AddRange(users);
        context.SaveChanges();
    }
}

