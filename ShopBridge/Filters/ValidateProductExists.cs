using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBridge.DataAccess.GenericRepository.Contracts;
using ShopBridge.Domain.Dto;
using ShopBridge.Domain.Entities;
using ShopBridge.Service.Logging;

namespace ShopBridge.Filters;

public class ValidateProductExists : IAsyncActionFilter
{
    private readonly IRepository _repository;
    private readonly ILoggerManager _logger;

    public ValidateProductExists(IRepository repository, ILoggerManager logger)
    {
        _repository = repository; _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var id = (int)context.ActionArguments[context.ActionArguments.Keys.Where(x => x.Equals("productId") || x.Equals("productId")).SingleOrDefault()];

        var product = await _repository.GetAsync<Product>("SELECT * FROM Product WHERE Id = @id", new { id }, commandType: System.Data.CommandType.Text);
        if (product is null)
        { 
            _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
            var response = new ObjectResult(new ResponseModel
            {
                StatusCode = 404,
                Message = $"Product with id: {id} doesn't exist in the database."
            });
            context.Result = response;
        }
        else
        {
            context.HttpContext.Items.Add("product", product); 
            await next();
        }
    }
}