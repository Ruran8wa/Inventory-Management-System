namespace InventoryManagementSystem;

class Program
{
    static readonly InventoryManager Manager = new();

    static void Main()
    {
        SeedSampleData();

        var running = true;
        while (running)
        {
            PrintMenu();
            var choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1": AddProduct(); break;
                    case "2": ViewAllProducts(); break;
                    case "3": ViewProductsByCategory(); break;
                    case "4": SellProduct(); break;
                    case "5": RestockProduct(); break;
                    case "6": ViewLowStock(); break;
                    case "7": ViewTotalValue(); break;
                    case "8":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1-8.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("==== Inventory Management System ====");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View All Products");
        Console.WriteLine("3. View Products by Category");
        Console.WriteLine("4. Sell Product");
        Console.WriteLine("5. Restock Product");
        Console.WriteLine("6. View Low Stock Alerts");
        Console.WriteLine("7. View Total Inventory Value");
        Console.WriteLine("8. Exit");
        Console.Write("Choose an option: ");
    }

    static void AddProduct()
    {
        Console.WriteLine("Product type: 1. Standard  2. Perishable  3. Digital");
        Console.Write("Choose a type: ");
        var type = Console.ReadLine();

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Category: ");
        var category = Console.ReadLine() ?? "";
        var price = ReadDecimal("Price: ");

        IInventoryItem item;
        switch (type)
        {
            case "2":
                var qty = ReadInt("Quantity in stock: ");
                var threshold = ReadInt("Restock threshold: ");
                var expiration = ReadDate("Expiration date (yyyy-MM-dd): ");
                item = new PerishableProduct(name, category, price, qty, threshold, expiration);
                break;
            case "3":
                item = new DigitalProduct(name, category, price);
                break;
            default:
                var stdQty = ReadInt("Quantity in stock: ");
                var stdThreshold = ReadInt("Restock threshold: ");
                item = new StandardProduct(name, category, price, stdQty, stdThreshold);
                break;
        }

        Manager.AddProduct(item);
        Console.WriteLine($"Added '{item.Name}'.");
    }

    static void ViewAllProducts()
    {
        var products = Manager.GetAllProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("No products in inventory.");
            return;
        }

        foreach (var p in products)
            Console.WriteLine(p);
    }

    static void ViewProductsByCategory()
    {
        Console.Write("Category: ");
        var category = Console.ReadLine() ?? "";
        var products = Manager.GetProductsByCategory(category);

        if (products.Count == 0)
        {
            Console.WriteLine("No products found in that category.");
            return;
        }

        foreach (var p in products)
            Console.WriteLine(p);
    }

    static void SellProduct()
    {
        Console.Write("Product name: ");
        var name = Console.ReadLine() ?? "";
        var item = Manager.FindProductByName(name);
        if (item is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        var quantity = ReadInt("Quantity to sell: ");
        item.SellItem(quantity);
        Console.WriteLine($"Sold {quantity} unit(s) of '{item.Name}'.");
    }

    static void RestockProduct()
    {
        Console.Write("Product name: ");
        var name = Console.ReadLine() ?? "";
        var item = Manager.FindProductByName(name);
        if (item is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        var quantity = ReadInt("Quantity to restock: ");
        item.RestockItem(quantity);
        Console.WriteLine($"Restocked '{item.Name}'. New quantity: {item.QuantityInStock}.");
    }

    static void ViewLowStock()
    {
        var products = Manager.GetProductsNeedingRestock();
        if (products.Count == 0)
        {
            Console.WriteLine("No products need restocking.");
            return;
        }

        foreach (var p in products)
            Console.WriteLine(p);
    }

    static void ViewTotalValue()
    {
        Console.WriteLine($"Total inventory value: ${Manager.GetTotalInventoryValue():F2}");
    }

    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var value))
                return value;
            Console.WriteLine("Please enter a valid whole number.");
        }
    }

    static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out var value))
                return value;
            Console.WriteLine("Please enter a valid number.");
        }
    }

    static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out var value))
                return value;
            Console.WriteLine("Please enter a valid date (yyyy-MM-dd).");
        }
    }

    static void SeedSampleData()
    {
        Manager.AddProduct(new StandardProduct("USB Cable", "Electronics", 5.99m, 50, 10));
        Manager.AddProduct(new PerishableProduct("Milk", "Groceries", 2.49m, 8, 10, DateTime.Now.AddDays(5)));
        Manager.AddProduct(new DigitalProduct("E-Book: Learn C#", "Software", 12.99m));
    }
}
