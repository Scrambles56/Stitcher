namespace Customers.App.Customers;

[ExtendObjectType("Query")]
public class CustomerQueries
{
    private readonly CustomersRepository _customersRepository;

    public CustomerQueries(CustomersRepository customersRepository)
    {
        _customersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
    }
    
    public Task<Customer> GetCustomer(int customerId) => _customersRepository.GetCustomer(customerId);
    
    public Task<List<Customer>> GetCustomers(IEnumerable<int> customerIds) => _customersRepository.GetCustomers(customerIds);
}