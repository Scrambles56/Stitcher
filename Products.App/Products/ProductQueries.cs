using HotChocolate.Types;
using JetBrains.Annotations;

namespace Products.App.Products;

[ExtendObjectType("Query")]
public class ProductQueries
{
    private readonly ProductRepository _repository;

    public ProductQueries(ProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    [UsedImplicitly]
    public IEnumerable<Product> GetAllProducts() => _repository.GetAllProducts();

    [UsedImplicitly]
    public Product GetProduct(int productId) => _repository.GetProduct(productId);
    
    [UsedImplicitly]
    public IEnumerable<Product> GetProducts(IEnumerable<int> productIds) => _repository.GetProducts(productIds);
}