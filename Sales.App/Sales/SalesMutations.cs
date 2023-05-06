namespace Sales.App.Sales;

[ExtendObjectType("Mutation")]
public class SalesMutations
{
    private readonly SalesRepository _repository;

    public SalesMutations(SalesRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Task<Transaction> PurchaseItem(int customerId, int productId, decimal amount) => _repository.PurchaseItem(customerId, productId, amount);

    public Task<Transaction> RefundTransaction(int transactionId) => _repository.RefundTransaction(transactionId);
}