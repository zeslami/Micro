namespace P1.Application.Contracts.DTOs.Products;

/// <summary>Maps to the SQL View vw_ProductsWithGroup.</summary>
public class ProductWithGroupDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int ProductGroupId { get; set; }
    public string GroupName { get; set; } = null!;
}
