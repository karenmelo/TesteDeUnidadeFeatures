namespace Features.Tests._2___Fixtures;

[Collection(nameof(CustomerCollection))]
public class CustomerInvalidTest
{
    readonly CustomerTestsFixture _customerTestsFixture;

    public CustomerInvalidTest(CustomerTestsFixture customerTestsFixture)
    {
        _customerTestsFixture = customerTestsFixture;
    }

    [Fact(DisplayName = "New customer invalid")]
    [Trait("Category", "CustomerFolder Fixtures Invalid")]
    public void Customer_NewCustomer_MustNotBeValid()
    {
        //arrange
        var customer = _customerTestsFixture.GenerateInvalidCustomer();
        
        //act
        var result = customer.IsValid();
        
        //assert
        Assert.False(result);
        Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
    }
}