namespace Features.Customer;

public interface ICustomerService: IDisposable
{
    IEnumerable<Customer> GetAllAssets();
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
    void Inactivate(Customer customer);
    
}