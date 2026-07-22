namespace InventoryManagementSystem;

public interface IInventoryItem
{
    string Name { get; }
    string Category { get; }
    decimal Price { get; }
    int QuantityInStock { get; }
    int RestockThreshold { get; }

    void RestockItem(int quantity);
    void SellItem(int quantity);
    bool NeedsRestock();
}
