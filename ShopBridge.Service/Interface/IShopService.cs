using ShopBridge.Domain.Dto;
using ShopBridge.Domain.Entities;

namespace ShopBridge.Service.Interface;

public interface IShopService
{
    Task<ResponseModel> AddProduct(ProductCreateDto productCreate);
    Task<ResponseModel> UpdateProduct(int productId, ProductUpdateDto productUpdate);
    Task<IList<Product?>?> GetProducts(int pageNumber, int pageSize);
    Task<ResponseModel> DeleteProduct(int productId);

}
