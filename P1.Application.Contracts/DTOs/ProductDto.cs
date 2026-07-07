using P1.Domain.Entities; // مسیر واقعی انتیتی‌های پروژه‌ات

namespace P1.Application.Contracts.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ProductGroupId { get; set; }

    public static ProductDto FromEntity(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        ProductGroupId = product.ProductGroupId
    };
}

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ProductGroupId { get; set; }

    public Product ToEntity() => new()
    {
        Name = this.Name,
        Price = this.Price,
        ProductGroupId = this.ProductGroupId
    };
}

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ProductGroupId { get; set; }

    public void UpdateEntity(Product product)
    {
        product.Name = this.Name;
        product.Price = this.Price;
        product.ProductGroupId = this.ProductGroupId;
    }
}
