using ShopBridge.DataAccess.GenericRepository;
using ShopBridge.DataAccess.GenericRepository.Contracts;
using ShopBridge.Domain.Dto;
using ShopBridge.Service.Interface;
using System.Data;

namespace ShopBridge.Service.Implementation;

public sealed class ShopService : IShopService
{
    private readonly IRepository _repo;

    public ShopService(IRepository repo) 
    {
        _repo = repo;
    }

    public async Task<int> AddProduct(ProductCreateDto productCreate)
    {
        var productId = await _repo.CreateOrUpdateAsync(
           Queries.AddProductQuery,
             new
             {
                 Name = productCreate.Name,
                 Description = productCreate.Description,
                 Price = productCreate.Price
             }, commandType: CommandType.Text);

        return productId;
    }

    public async Task<bool> UpdateProduct(int productId, ProductUpdateDto productUpdate)
    {
        int affectedRows = await _repo.CreateOrUpdateAsync(
       Queries.UpdateProductQuery,
        new{
            Id = productId,
            Name = productUpdate.Name,
            Description = productUpdate.Description,
            Price = productUpdate.Price
        }, commandType: CommandType.Text);

        return affectedRows > 0; 
    }
    
    public async Task<bool> DeleteProduct(int productId)   
    {
        int affectedRows = await _repo.CreateOrUpdateAsync(
        Queries.DeleteProductQuery,
        new { productId}, commandType: CommandType.Text);

        return affectedRows > 0; 
    }

    public async Task<IList<Product?>?> GetProducts(int pageNumber, int pageSize)
    {
        try
        {
            var products = await _repo.GetAllAsync<Product>(Queries.GetProductsQuery, 
                new { PageNumber = pageNumber, PageSize = pageSize }, CommandType.Text);
            return products?.ToList();
        } 
        catch (Exception)
        {
            throw;
        }

    }

}
