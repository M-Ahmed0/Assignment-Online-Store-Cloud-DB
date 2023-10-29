using DAL;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;
using Service.Mapping;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(EntityBaseRepository<>));
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddAutoMapper(typeof(MappingProfile).Assembly); 
    })
    .Build();

host.Run();


