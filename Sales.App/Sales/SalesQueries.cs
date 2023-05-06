namespace Sales.App.Sales;

[ExtendObjectType("Query")]
public class SalesQueries
{
    private readonly SalesRepository _repository;

    public SalesQueries(SalesRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Task<SalesData> GetSalesData() => _repository.GetSalesData();
    
    public Task<List<Transaction>> GetTransactionsForCustomer(int customerId) => _repository.GetTransactionsForCustomer(customerId);
}