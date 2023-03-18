using Microsoft.Extensions.DependencyInjection;
using ShopBridge.DataAccess.Data;
using ShopBridge.DataAccess.GenericRepository.Abstracts;
using ShopBridge.DataAccess.GenericRepository.Contracts;

namespace ShopBridge.Service;

public static class DataAccessExtension
{
    public static void RegisterDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>(); 
        services.AddSingleton<IRepository, Repository>();
    }
}
 