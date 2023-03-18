using ShopBridge.Domain.Dto;

namespace ShopBridge.Service.Interface;

public interface IShopService 
{
    Task<int> AddProduct(ProductCreateDto productCreate);
    Task<bool> UpdateProduct(int productId, ProductUpdateDto productUpdate);
    Task<IList<Product?>?> GetProducts(int pageNumber, int pageSize);
    Task<bool> DeleteProduct(int productId);

}
