using Microsoft.Extensions.DependencyInjection;
using ShopBridge.DataAccess.Data;
using ShopBridge.DataAccess.GenericRepository.Abstracts;
using ShopBridge.DataAccess.GenericRepository.Contracts;

namespace ShopBridge.Service;

public static class DataAccessExtension
{
    public static void RegisterDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<ShopBridgeDbContext>(); 
        services.AddSingleton<IRepository, Repository>();
    }
}
 