using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Hosting;
using Service;
using Service.Mapping;
using Azure.Storage.Queues;


var builder = WebApplication.CreateBuilder(args);

// Setup and configure services
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(EntityBaseRepository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

QueueMessageEncoding encodingOption = QueueMessageEncoding.Base64;

var options = new QueueClientOptions();
options.MessageEncoding = encodingOption;

builder.Services.AddSingleton(x => new QueueClient(builder.Configuration.GetConnectionString("AzureWebJobsStorage"), "order-queue-items", options) );

// Register services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IProductService, ProductService>();


// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the data seeder service
builder.Services.AddTransient<SeedData>();


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

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
