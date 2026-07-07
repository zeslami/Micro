namespace P1.Domain.Entities;


public partial class ProductWithGroupView
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int ProductGroupId { get; set; }

    public string GroupName { get; set; } = null!;
}
