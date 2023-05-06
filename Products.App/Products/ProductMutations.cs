using HotChocolate.Types;
using JetBrains.Annotations;

namespace Products.App.Products;

[ExtendObjectType("Mutation")]
public class ProductMutations
{
    private readonly ProductRepository _repository;

    public ProductMutations(ProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    [UsedImplicitly]
    public Task<Product> AddProduct(string productName) => _repository.AddProduct(productName);
    
    [UsedImplicitly]
    public Task<Product> DeleteProduct(int productId) => _repository.DeleteProduct(productId);
}