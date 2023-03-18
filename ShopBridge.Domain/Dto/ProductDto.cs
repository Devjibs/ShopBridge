using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.Dto;

public class Product
{ 
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? AdditionalInfo { get; set; }
}

public abstract class ProductAddUpdateDto
{
    [Required, MinLength(2, ErrorMessage ="Product Name cannot be less than 2 characters")]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Price cannot be less than {1}")]
    public decimal Price { get; set; }
    public string? AdditionalInfo { get; set; }
}

public class ProductCreateDto : ProductAddUpdateDto
{ 

}

public class ProductUpdateDto : ProductAddUpdateDto
{ 

}

