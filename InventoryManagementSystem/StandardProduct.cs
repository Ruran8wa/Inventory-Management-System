namespace InventoryManagementSystem;

public class StandardProduct : Product
{
    public StandardProduct(string name, string category, decimal price, int quantityInStock, int restockThreshold)
        : base(name, category, price, quantityInStock, restockThreshold)
    {
    }
}
