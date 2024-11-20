using Features.CustomerFolder;

namespace Features.Tests._1___Traits;

public class CustomerTests
{
    [Fact(DisplayName = "New CustomerFolder Valid")]
    [Trait("Category", "CustomerFolder Trait Valid")]
    public void Customer_NewCustomer_MustBeValid()
    {
        //arrange
        var customer = new Customer(
            Guid.NewGuid(),
            "Karen",
            "Melo",
            DateTime.Now.AddYears(-30),
            "karen@softka.com",
            true,
            DateTime.Now);
        
        //act
        var result = customer.IsValid();
        
        //assert
        Assert.True(result);
        Assert.Equal(0, customer.ValidationResult.Errors.Count);
    }

    [Fact(DisplayName = "New customer invalid")]
    [Trait("Category", "CustomerFolder Trait Invalid")]
    public void Customer_NewCustomer_MustNotBeValid()
    {
        //arrange
        var customer = new Customer(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now, 
            "karen2softka.com",
            true,
            DateTime.Now);
        
        //act
        var result = customer.IsValid();
        
        //assert
        Assert.False(result);
        Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
    }
}