using Microsoft.AspNetCore.Diagnostics;
using NLog;
using ShopBridge.Domain.Entities;
using ShopBridge.Filters;
using System.Net;
using ILogger = NLog.ILogger;

namespace ShopBridge.Extensions;

public static class MiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        ILogger logger = LogManager.GetCurrentClassLogger();
        app.UseExceptionHandler(
            appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(
                            new ResponseModel()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                            }.ToString());
                    }
                });
            });
    }
    public static void RegisterValidationFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<ValidateProductExists>();
    }
}
