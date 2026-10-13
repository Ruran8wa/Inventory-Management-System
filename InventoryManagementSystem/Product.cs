namespace InventoryManagementSystem;

public abstract class Product : IInventoryItem
{
    public string Name { get; }
    public string Category { get; }
    public decimal Price { get; }
    public int QuantityInStock { get; protected set; }
    public int RestockThreshold { get; }

    protected Product(string name, string category, decimal price, int quantityInStock, int restockThreshold)
    {
        Name = name;
        Category = category;
        Price = price;
        QuantityInStock = quantityInStock;
        RestockThreshold = restockThreshold;
    }

    public virtual void RestockItem(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Restock quantity must be greater than zero.");
        QuantityInStock += quantity;
    }

    public virtual void SellItem(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Sell quantity must be greater than zero.");
        if (quantity > QuantityInStock)
            throw new InsufficientStockException(Name, quantity, QuantityInStock);
        QuantityInStock -= quantity;
    }

    public virtual bool NeedsRestock() => QuantityInStock <= RestockThreshold;

    public override string ToString() =>
        $"{Name} [{Category}] - ${Price:F2} - Qty: {QuantityInStock}" + (NeedsRestock() ? " (LOW STOCK)" : "");
}
