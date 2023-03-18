namespace ShopBridge.DataAccess.GenericRepository;

public static class Queries
{
    public const string AddProductQuery = "INSERT INTO Product (Name, Description, Price, IsDeleted) " +
            "VALUES (@Name, @Description, @Price, 0); " +
            "SELECT CAST(SCOPE_IDENTITY() as int)";

    public const string UpdateProductQuery = "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price " +
        "WHERE Id = @Id";

    public const string DeleteProductQuery = "UPDATE Product SET IsDeleted = 1 WHERE Id = @productId AND IsDeleted = 0"; 

    public const string GetProductsQuery = "SELECT * FROM Product where COALESCE(IsDeleted, 0) = 0 ORDER BY Id OFFSET (@PageNumber - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
}
