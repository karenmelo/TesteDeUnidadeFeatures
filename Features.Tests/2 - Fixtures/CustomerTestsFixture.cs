using Features.CustomerFolder;

namespace Features.Tests._2___Fixtures;

[CollectionDefinition(nameof(CustomerCollection))]
public class CustomerCollection : ICollectionFixture<CustomerTestsFixture>
{
    
}

public class CustomerTestsFixture : IDisposable
{
    public Customer GenerateValidCustomer()
    {
        var customer = new Customer(
            Guid.NewGuid(),
            "Karen",
            "Melo",
            DateTime.Now.AddYears(-30),
            "karen@softka.com",
            true,
            DateTime.Now);

        return customer;
    } 
    
    public Customer GenerateInvalidCustomer()
    {
        var customer = new Customer(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now, 
            "karen2softka.com",
            true,
            DateTime.Now);
        
        return customer;
    } 

    
    public void Dispose()
    {
        
    }
    
}