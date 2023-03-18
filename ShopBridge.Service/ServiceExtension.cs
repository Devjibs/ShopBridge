using Microsoft.Extensions.DependencyInjection;
using ShopBridge.Service.Implementation;
using ShopBridge.Service.Interface;
using ShopBridge.Service.Logging;

namespace ShopBridge.Service;

public static class ServiceExtension
{
    public static void RegisterService(this IServiceCollection services)
    {
        services.AddScoped<ILoggerManager, LoggerManager>();
        services.AddScoped<IShopService, ShopService>();
    }
}
