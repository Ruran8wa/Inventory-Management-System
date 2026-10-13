namespace InventoryManagementSystem;

public class InventoryManager
{
    private readonly Dictionary<string, IInventoryItem> _products = new(StringComparer.OrdinalIgnoreCase);

    public void AddProduct(IInventoryItem item)
    {
        if (_products.ContainsKey(item.Name))
            throw new ArgumentException($"A product named '{item.Name}' already exists.");
        _products[item.Name] = item;
    }

    public bool RemoveProduct(string name) => _products.Remove(name);

    public List<IInventoryItem> GetAllProducts() => _products.Values.ToList();

    public List<IInventoryItem> GetProductsByCategory(string category) =>
        _products.Values
            .Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public List<IInventoryItem> GetProductsNeedingRestock() =>
        _products.Values.Where(p => p.NeedsRestock()).ToList();

    public IInventoryItem? FindProductByName(string name) =>
        _products.TryGetValue(name, out var item) ? item : null;

    public decimal GetTotalInventoryValue() =>
        _products.Values.Sum(p => p.Price * p.QuantityInStock);
}
