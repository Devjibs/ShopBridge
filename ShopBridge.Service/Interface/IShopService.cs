using ShopBridge.Domain.Dto;
using ShopBridge.Domain.Entities;

namespace ShopBridge.Service.Interface;

public interface IShopService
{
    Task<ResponseModel> AddProduct(ProductCreateDto productCreate);
    Task<ResponseModel> UpdateProduct(int productId, ProductUpdateDto productUpdate);
    Task<IList<Product?>?> GetProducts(string? ProductName = null, int pageNumber = 1, int pageSize = 10);
    Task<ResponseModel> DeleteProduct(int productId);

}
