namespace InventoryManagementSystem;

public class PerishableProduct : Product
{
    public DateTime ExpirationDate { get; }

    public PerishableProduct(string name, string category, decimal price, int quantityInStock, int restockThreshold, DateTime expirationDate)
        : base(name, category, price, quantityInStock, restockThreshold)
    {
        ExpirationDate = expirationDate;
    }

    public bool IsExpired() => DateTime.Now.Date > ExpirationDate.Date;

    public override void SellItem(int quantity)
    {
        if (IsExpired())
            throw new ExpiredProductException(Name, ExpirationDate);
        base.SellItem(quantity);
    }

    public override string ToString() =>
        base.ToString() + $" - Expires: {ExpirationDate:yyyy-MM-dd}" + (IsExpired() ? " [EXPIRED]" : "");
}
