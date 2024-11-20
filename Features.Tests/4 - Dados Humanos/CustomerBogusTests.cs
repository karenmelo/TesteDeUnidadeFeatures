﻿namespace Features.Tests._4___Dados_Humanos;

[Collection(nameof(CustomerBogusCollection))]
public class CustomerBogusTests
{
    private readonly CustomerTestsBogusFixture _customerTestsFixture;

    public CustomerBogusTests(CustomerTestsBogusFixture customerTestsFixture)
    {
        _customerTestsFixture = customerTestsFixture;
    }


    [Fact(DisplayName = "New CustomerFolder Valid")]
    [Trait("Category", "CustomerFolder Bogus Tests")]
    public void Customer_NewCustomer_MustBeValid()
    {
        // Arrange
        var customer = _customerTestsFixture.GenerateValidCustomer();

        // Act
        var result = customer.IsValid();

        // Assert 
        Assert.True(result);
        Assert.Equal(0, customer.ValidationResult.Errors.Count);
    }

    [Fact(DisplayName = "New CustomerFolder Invalid")]
    [Trait("Category", "CustomerFolder Bogus Tests")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var customer = _customerTestsFixture.GenerateInvalidCustomer();

        // Act
        var result = customer.IsValid();

        // Assert 
        Assert.False(result);
        Assert.NotEqual(0, customer.ValidationResult.Errors.Count);
    }
}