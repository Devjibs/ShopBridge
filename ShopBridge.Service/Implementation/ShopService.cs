using Microsoft.AspNetCore.Http;
using ShopBridge.DataAccess.GenericRepository;
using ShopBridge.DataAccess.GenericRepository.Contracts;
using ShopBridge.Domain.Dto;
using ShopBridge.Domain.Entities;
using ShopBridge.Service.Interface;
using System.Data;
using System.Net;

namespace ShopBridge.Service.Implementation;

public sealed class ShopService : IShopService
{
    private readonly IRepository _repo;
    private readonly IImageService _imageService;
    private readonly HttpContext _httpContext;

    public ShopService(IRepository repo, IImageService imageService, IHttpContextAccessor httpContext)
    {
        _repo = repo;
        _imageService = imageService;
        _httpContext = httpContext.HttpContext;
    }

    public async Task<ResponseModel> AddProduct(ProductCreateDto productCreate)
    {
        var image = productCreate.ProductImage is null ? "" : await _imageService.UploadImage(productCreate.ProductImage);
        var productId = await _repo.CreateOrUpdateAsync(
           Queries.AddProductQuery,
             new
             {
                 Name = productCreate.Name,
                 Description = productCreate.Description ?? "",
                 Price = productCreate.Price,
                 ProductImage = image
             }, commandType: CommandType.Text);
        if (productId > 0)
        {
            return new ResponseModel
            {
                Message = productCreate.Name + " has been added to the inventory successfully",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        return new ResponseModel
        {
            Message = "Unable to add product",
            StatusCode = (int)HttpStatusCode.BadRequest
        }; ;
    }

    public async Task<ResponseModel> UpdateProduct(int productId, ProductUpdateDto productUpdate)
    {
        var foundProduct = _httpContext.Items["product"] as Product;
        var image = productUpdate.ProductImage is null ? foundProduct?.ProductImage : await _imageService.UploadImage(productUpdate.ProductImage);

        int affectedRows = await _repo.CreateOrUpdateAsync(
       Queries.UpdateProductQuery,
        new
        {
            Id = productId,
            Name = productUpdate.Name ?? foundProduct?.Name,
            Description = productUpdate.Description ?? foundProduct?.Description,
            Price = productUpdate.Price ?? foundProduct?.Price,
            ProductImage = image
        }, commandType: CommandType.Text);

        if (affectedRows > 0)
        {
            return new ResponseModel
            {
                Message = "Product has been updated successfully",
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
        return new ResponseModel
        {
            Message = "Unable to update product",
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }

    public async Task<ResponseModel> DeleteProduct(int productId)
    {
        int affectedRows = await _repo.CreateOrUpdateAsync(
        Queries.DeleteProductQuery,
        new { productId }, commandType: CommandType.Text);

        if (affectedRows > 0)
        {
            return new ResponseModel
            {
                Message = "Product has been deleted successfully",
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }
        return new ResponseModel
        {
            Message = "Unable to delete product",
            StatusCode = (int)HttpStatusCode.BadRequest
        }; ;
    }

    public async Task<IList<Product?>?> GetProducts(int pageNumber, int pageSize)
    {
        var products = await _repo.GetAllAsync<Product>(Queries.GetProductsQuery,
            new { PageNumber = pageNumber, PageSize = pageSize }, CommandType.Text);
        return products?.ToList();
    }

}
