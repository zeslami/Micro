using P1.Domain.Entities;

namespace P1.Application.Contracts.DTOs;

public class ProductGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static ProductGroupDto FromEntity(ProductGroup group) => new()
    {
        Id = group.Id,
        Name = group.Name,
        Description = group.Description
    };
}

public class CreateProductGroupDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ProductGroup ToEntity() => new()
    {
        Name = this.Name,
        Description = this.Description
    };
}

public class UpdateProductGroupDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public void UpdateEntity(ProductGroup group)
    {
        group.Name = this.Name;
        group.Description = this.Description;
    }
}
