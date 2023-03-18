using Microsoft.AspNetCore.Http;
using ShopBridge.Service.Interface;
using ShopBridge.Service.Logging;

namespace ShopBridge.Service.Implementation;

public class ImageService : IImageService
{
    private readonly ILoggerManager _logger;

    public ImageService(ILoggerManager logger)
    {
        _logger = logger;
    }

    public async Task<string> UploadImage(IFormFile file)
    {
        try
        {

            string filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            // Converts image file into byte[]
            byte[] imageData = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(imageData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

}
