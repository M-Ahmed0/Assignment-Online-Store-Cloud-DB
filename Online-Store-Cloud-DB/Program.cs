using DAL;
using DAL.Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Setup and configure services
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(EntityBaseRepository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register services
builder.Services.AddScoped<IOrderService, OrderService>();

// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the data seeder service
builder.Services.AddTransient<SeedData>();

var app = builder.Build();

// Ensure database is created and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<SeedData>();

    try
    {
        context.Database.EnsureCreated();
        // Seeding
        seeder.InitializeAsync().Wait();
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    }
}

// Configure middleware and HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
