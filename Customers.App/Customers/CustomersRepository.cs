namespace Customers.App.Customers;

public class CustomersRepository
{
    private readonly List<Customer> _customer = new();

    public Task<Customer> GetCustomer(int customerId)
    {
        var customer = _customer.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer == null)
        {
            throw new Exception($"Customer with id {customerId} not found");
        }

        return Task.FromResult(customer);
    }
    
    public Task<List<Customer>> GetCustomers(IEnumerable<int> customerIds)
    {
        var customers = _customer.Where(c => customerIds.Contains(c.CustomerId)).ToList();
        return Task.FromResult(customers);
    }
    
    public Task<Customer> AddCustomer(string firstName, string lastName)
    {
        var existingCustomer = _customer.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);
        if (existingCustomer != null)
        {
            throw new Exception($"Customer with name {firstName} {lastName} already exists");
        }
        
        var maxId = _customer.Any() ? 
            _customer.Max(c => c.CustomerId)
            : 1;
        var newCustomer = new Customer(maxId + 1, firstName, lastName);
        _customer.Add(newCustomer);

        return Task.FromResult(newCustomer);
    }
    
    public Task<Customer> DeleteCustomer(int customerId)
    {
        var customer = _customer.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer == null)
        {
            throw new Exception($"Customer with id {customerId} does not exist");
        }
        
        _customer.Remove(customer);
        return Task.FromResult(customer);
    }
}

public class Customer
{
    public Customer(int maxId, string firstName, string lastName)
    {
        CustomerId = maxId;
        FirstName = firstName;
        LastName = lastName;
    }

    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}