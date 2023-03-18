using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.DataAccess.Data;
using ShopBridge.Domain.Dto;
using ShopBridge.Service.Interface;
using Xunit;

namespace ShopBridge.Test; 

public sealed class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IShopService> _shopServiceMock;
    private readonly Mock<ShopBridgeDbContext> _dapperMock;

    public ProductsControllerTests()
    {
        _dapperMock = new Mock<ShopBridgeDbContext>();

        _shopServiceMock = new Mock<IShopService>();
        _controller = new ProductsController(_shopServiceMock.Object);

    }

    [Fact]
    public void AddProduct_ReturnsOk()
    {
        // Arrange
        var product = new ProductCreateDto
        {
            Name = "Test Product",
            Description = "This is a test product.",
            Price = 10.0m,
            AdditionalInfo = "Test additional info"
        };

        _shopServiceMock.Setup(x => x.AddProduct(product).Result)
            .Returns(1);

        // Act
        var result = _controller.AddProduct(product).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var createdProduct = Assert.IsType<ProductCreateDto>(okResult.Value);

        Assert.Equal(product.Name, createdProduct.Name);
        Assert.Equal(product.Description, createdProduct.Description);
        Assert.Equal(product.Price, createdProduct.Price);
        Assert.Equal(product.AdditionalInfo, createdProduct.AdditionalInfo);
    }

    [Fact]
    public void UpdateProduct_ReturnsOk()
    {
        // Arrange
        int productId = 1;
        var product = new ProductUpdateDto
        {
            Name = "Test Product",
            Description = "This is a test product.",
            Price = 10.0m,
            AdditionalInfo = "Test additional info"
        };

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).Result)
            .Returns(true);

        // Act
        var result = _controller.UpdateProduct(1, product).Result;

        // Assert
        Assert.IsType<OkObjectResult>(result);

    }

    [Fact]
    public void UpdateProduct_ReturnsNotFound()
    {
        // Arrange
        int productId = 1;
        var product = new ProductUpdateDto
        {
            Name = "Test Product",
            Description = "This is a test product.",
            Price = 10.0m,
            AdditionalInfo = "Test additional info"
        };

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).Result)
            .Returns(false);

        // Act
        var result = _controller.UpdateProduct(productId, product).Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
