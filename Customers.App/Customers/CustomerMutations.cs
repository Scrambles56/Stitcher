namespace Customers.App.Customers;

[ExtendObjectType("Mutation")]
public class CustomerMutations
{
    private readonly CustomersRepository _customersRepository;

    public CustomerMutations(CustomersRepository customersRepository)
    {
        _customersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
    }
    
    public Task<Customer> AddCustomer(string firstName, string lastName) => _customersRepository.AddCustomer(firstName, lastName);
    public Task<Customer> DeleteCustomer(int customerId) => _customersRepository.DeleteCustomer(customerId);
}