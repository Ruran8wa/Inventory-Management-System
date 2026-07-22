namespace InventoryManagementSystem;

public class InsufficientStockException : Exception
{
    public InsufficientStockException(string productName, int requested, int available)
        : base($"Cannot sell {requested} unit(s) of '{productName}': only {available} in stock.")
    {
    }
}

public class ExpiredProductException : Exception
{
    public ExpiredProductException(string productName, DateTime expirationDate)
        : base($"Cannot sell '{productName}': it expired on {expirationDate:yyyy-MM-dd}.")
    {
    }
}
