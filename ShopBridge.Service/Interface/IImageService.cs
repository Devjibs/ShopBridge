using Microsoft.AspNetCore.Http;

namespace ShopBridge.Service.Interface;

public interface IImageService
{
    Task<string> UploadImage(IFormFile file);
}
