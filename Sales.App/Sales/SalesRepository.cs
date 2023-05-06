namespace Sales.App.Sales;

public class SalesRepository
{
    private readonly List<Transaction> _transactions = new();
    
    public Task<Transaction> PurchaseItem(int customerId, int productId, decimal amount)
    {
        var maxId = _transactions.Any() 
            ? _transactions.Max(t => t.Id)
            : 1;
        
        var transaction = new Transaction
        {
            Id = maxId + 1,
            CustomerId = customerId,
            ProductId = productId,
            TransactionType = TransactionType.Sale,
            TransactionDate = DateTime.UtcNow,
            Amount = amount
        };
        
        _transactions.Add(transaction);
        return Task.FromResult(transaction);
    }

    public Task<Transaction> RefundTransaction(int transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction == null)
        {
            throw new Exception($"Transaction with id {transactionId} does not exist");
        }
        
        var refundTransaction = new Transaction
        {
            Id = _transactions.Max(t => t.Id) + 1,
            CustomerId = transaction.CustomerId,
            ProductId = transaction.ProductId,
            TransactionType = TransactionType.Refund,
            TransactionDate = DateTime.UtcNow,
            Amount = transaction.Amount
        };
        
        _transactions.Add(refundTransaction);
        return Task.FromResult(refundTransaction);
    }

    public Task<SalesData> GetSalesData()
    {
        var grossRevenue = _transactions.Where(t => t.TransactionType == TransactionType.Sale).Sum(t => (int) t.Amount);
        var salesReturns = _transactions.Where(t => t.TransactionType == TransactionType.Refund).Sum(t => (int) t.Amount);
        var netSales = grossRevenue - salesReturns;
        
        return Task.FromResult(new SalesData
        {
            GrossRevenue = grossRevenue,
            SalesReturns = salesReturns,
            NetSales = netSales
        });
    }
    
    public Task<List<Transaction>> GetTransactionsForCustomer(int customerId)
    {
        var transactions = _transactions.Where(t => t.CustomerId == customerId).ToList();
        return Task.FromResult(transactions);
    }
}



public class SalesData
{
    public int GrossRevenue { get; set; }
    public int SalesReturns { get; set; }
    public int NetSales { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
}

public enum TransactionType
{
    Sale,
    Refund
}