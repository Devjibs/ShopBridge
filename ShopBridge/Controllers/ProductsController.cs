using Microsoft.AspNetCore.Mvc;
using ShopBridge.Domain.Dto;
using ShopBridge.Filters;
using ShopBridge.Service.Interface;

namespace ShopBridge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private  readonly IShopService _shopService;

    public ProductsController(IShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpPost, Route("AddProduct")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> AddProduct([FromForm] ProductCreateDto productDto) 
    {
       var productCreation = await _shopService.AddProduct(productDto);
        if (productCreation.StatusCode == 200)
        {
            return Ok(productCreation);
        }
        return BadRequest(productCreation);
    }
    
    [HttpPut, Route("UpdateProduct")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateProductExists))]
    public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductUpdateDto productDto) 
    {
       var productUpdate = await _shopService.UpdateProduct(productId, productDto); 
        if (productUpdate.StatusCode == 204)
        { 
            return Ok(productUpdate);
        }
        return BadRequest(productUpdate);
    }
    
    [HttpDelete, Route("DeleteProduct")]
    [ServiceFilter(typeof(ValidateProductExists))] 
    public async Task<IActionResult> DeleteProduct(int productId) 
    {
       var productDeleted = await _shopService.DeleteProduct(productId);
        if (productDeleted.StatusCode == 204)
        {
            return Ok(productDeleted);
        }
        return BadRequest(productDeleted);  
    }
     
    [HttpPost, Route("GetProducts")] 
    public async Task<IActionResult> GetProducts(int pageNo = 1, int pageSize = 10)  
    { 
        var products = await _shopService.GetProducts(pageNo, pageSize);
        return products is null ? NotFound(products) : Ok(products);
    } 
}