namespace P2.Application.Contracts.DTOs.Products;

public class UpdateProductDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int ProductGroupId { get; set; }
}
