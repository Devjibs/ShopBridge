namespace ShopBridge.Domain.Dto;

public class Product
{ 
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public abstract class ProductAddUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class ProductCreateDto : ProductAddUpdateDto
{ 

}

public class ProductUpdateDto : ProductAddUpdateDto
{ 

}

