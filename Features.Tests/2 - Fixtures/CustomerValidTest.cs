namespace Features.Tests._2___Fixtures;

[Collection(nameof(CustomerCollection))]
public class CustomerValidTest
{
    readonly CustomerTestsFixture _customerTestsFixture;

    public CustomerValidTest(CustomerTestsFixture customerTestsFixture)
    {
        _customerTestsFixture = customerTestsFixture;
    }

    [Fact(DisplayName = "New CustomerFolder Valid")]
    [Trait("Category", "CustomerFolder Fixtures Valid")]
    public void Customer_NewCustomer_MustBeValid()
    {
        //arrange
        var customer = _customerTestsFixture.GenerateValidCustomer();
        //act
        var result = customer.IsValid();
        
        //assert
        Assert.True(result);
        Assert.Equal(0, customer.ValidationResult.Errors.Count);
    }
}