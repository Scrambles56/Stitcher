namespace Products.App.Products;

public class ProductRepository
{
    private readonly List<Product> _products =
            new()
            {
                new(1, "Flat White"),
                new(2, "Latte"),
                new(3, "Cappuccino"),
                new(4, "Espresso"),
                new(5, "Mochaccino"),
                new(6, "Hot Chocolate"),
                new(7, "Tea"),
            };
    
    public IEnumerable<Product> GetAllProducts() => _products;

    public IEnumerable<Product> GetProducts(IEnumerable<int> productIds)
    {
        var products = _products.Where(p => productIds.Contains(p.ProductId));
        return products;
    }

    public Product GetProduct(int productId)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
        {
            throw new Exception($"Product with id {productId} does not exist");
        }
        
        return product;
    }

    public Task<Product> AddProduct(string productName)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Name == productName);
        if (existingProduct != null)
        {
            throw new Exception($"Product with name {productName} already exists");
        }
        
        var maxId = _products.Max(p => p.ProductId);
        var newProduct = new Product(maxId + 1, productName);
        _products.Add(newProduct);

        return Task.FromResult(newProduct);
    }
    
    public Task<Product> DeleteProduct(int productId)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
        {
            throw new Exception($"Product with id {productId} does not exist");
        }
        
        _products.Remove(product);

        return Task.FromResult(product);
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }

    public Product(int productId, string name)
    {
        ProductId = productId;
        Name = name;
    }
}