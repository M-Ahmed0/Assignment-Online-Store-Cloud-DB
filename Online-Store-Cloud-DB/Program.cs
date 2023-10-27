using DAL;
using DAL.Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddControllers();

// Registering repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(EntityBaseRepository<>));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Registering services
builder.Services.AddScoped<IOrderService, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SeedOrder>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var seeder = scope.ServiceProvider.GetRequiredService<SeedOrder>();

    context.Database.EnsureCreated();

    // Seeding
    seeder.InitializeAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
