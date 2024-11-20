using Features.CustomerFolder;

namespace Features.Tests._2___Fixtures;

public class CustomerFixturesTests
{
    public Customer ValidCustomer;
    public Customer InvalidCustomer;

    public CustomerFixturesTests()
    {
        ValidCustomer = new Customer(
            Guid.NewGuid(),
            "Karen",
            "Melo",
            DateTime.Now.AddYears(-30),
            "karen@softka.com",
            true,
            DateTime.Now);
        
        InvalidCustomer = new Customer(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now, 
            "karen2softka.com",
            true,
            DateTime.Now);
    }
    
    [Fact(DisplayName = "New CustomerFolder Valid")]
    [Trait("Category", "CustomerFolder Fixtures Valid")]
    public void Customer_NewCustomer_MustBeValid()
    {
        //act
        var result = ValidCustomer.IsValid();
        
        //assert
        Assert.True(result);
        Assert.Equal(0, ValidCustomer.ValidationResult.Errors.Count);
    }

    [Fact(DisplayName = "New customer invalid")]
    [Trait("Category", "CustomerFolder Fixtures Invalid")]
    public void Customer_NewCustomer_MustNotBeValid()
    {
        //act
        var result = InvalidCustomer.IsValid();
        
        //assert
        Assert.False(result);
        Assert.NotEqual(0, InvalidCustomer.ValidationResult.Errors.Count);
    }
}