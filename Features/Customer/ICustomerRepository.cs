using Features.Core;

namespace Features.Customer;

public interface ICustomerRepository: IRepository<Customer>
{
    Customer GetByEmail(string email);
    
}