namespace P1.Application.Contracts.DTOs.Products;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public DateTime CreatedAt { get; set; }
}
