using DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

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

// Configure the HTTP request pipeline.

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/products", (ApplicationDbContext context) => context.Products.ToList());   

app.Run();

//static void CreateDatabase<TContext>(WebApplication app)
//    where TContext : DbContext
//{
//    using var scope = app.Services.CreateScope();

//    var context = scope.ServiceProvider.GetRequiredService<TContext>();

//    context.Database.EnsureCreated();
//}

