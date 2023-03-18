using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Domain.Dto;
using ShopBridge.Domain.Entities;
using ShopBridge.Service.Interface;
using System.Net;

namespace ShopBridge.Test;

public sealed class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly Mock<IShopService> _shopServiceMock;

    public ProductsControllerTests()
    {
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
            Price = 10.0m
        };

        _shopServiceMock.Setup(x => x.AddProduct(product).Result)
            .Returns(new ResponseModel
            {
                Message = product.Name + " has been added to the inventory successfully",
                StatusCode = (int)HttpStatusCode.OK
            });

        // Act
        var result = _controller.AddProduct(product).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<ResponseModel>(okResult.Value);
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
            Price = 10.0m
        };

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).Result)
            .Returns(new ResponseModel
            {
                Message = "Product has been updated successfully",
                StatusCode = (int)HttpStatusCode.NoContent
            });

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
            Price = 10.0m
        };

        _shopServiceMock.Setup(x => x.UpdateProduct(productId, product).Result)
            .Returns(new ResponseModel
            {
                Message = "Unable to update product",
                StatusCode = (int)HttpStatusCode.BadRequest
            });

        // Act
        var result = _controller.UpdateProduct(productId, product).Result;

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
