using Features.Core;

namespace Features.CustomerFolder;

public interface ICustomerRepository: IRepository<Customer>
{
    Customer GetByEmail(string email);
    
}