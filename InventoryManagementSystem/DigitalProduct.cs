namespace InventoryManagementSystem;

public class DigitalProduct : Product
{
    // Digital products have unlimited stock, so QuantityInStock is not used to
    // track availability; it stays at 0 and never affects selling or restocking.
    public DigitalProduct(string name, string category, decimal price)
        : base(name, category, price, quantityInStock: 0, restockThreshold: 0)
    {
    }

    public override void RestockItem(int quantity)
    {
        // No-op: digital products are never physically restocked.
    }

    public override void SellItem(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Sell quantity must be greater than zero.");
        // No-op: stock never decreases, sale always succeeds.
    }

    public override bool NeedsRestock() => false;

    public override string ToString() => $"{Name} [{Category}] - ${Price:F2} - Qty: Unlimited";
}
