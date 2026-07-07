namespace P1.Domain.Entities;

public partial class ProductByGroupResult
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
