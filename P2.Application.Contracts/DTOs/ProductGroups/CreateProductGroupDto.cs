namespace P2.Application.Contracts.DTOs.ProductGroups;

public class CreateProductGroupDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
