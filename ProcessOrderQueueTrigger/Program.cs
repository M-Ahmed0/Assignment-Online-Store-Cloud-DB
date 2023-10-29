using DAL.Repositories;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Mapping;
using Service;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(EntityBaseRepository<>));
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
    })
    .Build();

host.Run();
