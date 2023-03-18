using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.DataAccess.Data;
using ShopBridge.Domain.Dto;
using ShopBridge.Service.Interface;
using Xunit;

namespace ShopBridge.Tests;

public sealed class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IShopService> _shopServiceMock;
    private readonly Mock<DapperContext> _dapperMock;

    public ProductsControllerTests()
    {
        _dapperMock = new Mock<DapperContext>();

        _shopServiceMock = new Mock<IShopService>();
        _controller = new ProductsController(_shopServiceMock.Object);

    }

    [Fact]
    public void DatabaseConnection_IsCorrect() 
    {
        var result = _dapperMock.SetupGet(x => x.CreateSqlServerConnection().ConnectionString)
            .Returns("Data Source=(local);Initial Catalog=ShopBridgeTest;Integrated Security=True");

        Assert.True(result.Equals(true));    
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

        _shopServiceMock.Setup(x => x.AddProduct(product).GetAwaiter().GetResult())
            .Returns(1);

        // Act
        var result = _controller.AddProduct(product);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var createdProduct = Assert.IsType<Product>(okResult.Value);

        Assert.Equal(1, createdProduct.Id);
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

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).GetAwaiter().GetResult())
            .Returns(true);

        // Act
        var result = _controller.UpdateProduct(1, product);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedProduct = Assert.IsType<Product>(okResult.Value);

        Assert.Equal(productId, updatedProduct.Id);
        Assert.Equal(product.Name, updatedProduct.Name);
        Assert.Equal(product.Description, updatedProduct.Description);
        Assert.Equal(product.Price, updatedProduct.Price);
        Assert.Equal(product.AdditionalInfo, updatedProduct.AdditionalInfo);
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

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).GetAwaiter().GetResult())
            .Returns(false);

        // Act
        var result = _controller.UpdateProduct(productId, product);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
