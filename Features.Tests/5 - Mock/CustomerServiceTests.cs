using Features.CustomerFolder;
using Features.Tests._4___Dados_Humanos;
using MediatR;
using Moq;

namespace Features.Tests._5___Mock;

[Collection(nameof(CustomerBogusCollection))]
public class CustomerServiceTests
{
    readonly CustomerTestsBogusFixture _customerTestsBogusFixture;

    public CustomerServiceTests(CustomerTestsBogusFixture customerTestsBogusFixture)
    {
        _customerTestsBogusFixture = customerTestsBogusFixture;
    }

    [Fact(DisplayName = "Add Customer Successfully")]
    [InlineData("Category", "Customer Service Mock Tests")]
    public void CustomerService_Add_MustBeExecuteSuccessfully()
    {
        //arrange
        var customer = _customerTestsBogusFixture.GenerateValidCustomer();
        var customerRepo = new Mock<ICustomerRepository>();
        var mediator = new Mock<IMediator>();
        var customerService = new CustomerService(customerRepo.Object, mediator.Object);
        
        //act
        customerService.Add(customer);
        
        //assert
        Assert.True(customer.IsValid()); //isso deve ser validado dentro do metodo Add.
        customerRepo.Verify(m => m.Add(customer), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<INotification>(),CancellationToken.None), Times.Once);
    }   
    
    [Fact(DisplayName = "Add Customer Failed")]
    [InlineData("Category", "Customer Service Mock Tests")]
    public void CustomerService_Add_MustBeExecuteFailed()
    {
        //arrange
        var customer = _customerTestsBogusFixture.GenerateInvalidCustomer();
        var customerRepo = new Mock<ICustomerRepository>();
        var mediator = new Mock<IMediator>();
        var customerService = new CustomerService(customerRepo.Object, mediator.Object);
        
        //act
        customerService.Add(customer);
        
        //assert
        Assert.False(customer.IsValid()); //isso deve ser validado dentro do metodo Add.
        customerRepo.Verify(m => m.Add(customer), Times.Never);
        mediator.Verify(m => m.Publish(It.IsAny<INotification>(),CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Get Active Customers")]
    [Trait("Category", "Customer Service Mock Tests")]
    public void CustomerService_GetAllActiveCustomers_MustReturnAllActiveCustomers()
    {
        //arrange
        var customerRepo = new Mock<ICustomerRepository>();
        var mediator = new Mock<IMediator>();
        
        customerRepo.Setup(c => c.GetAll())
                    .Returns(_customerTestsBogusFixture.GetVariedCustomers());
        
        var customerService = new CustomerService(customerRepo.Object, mediator.Object);
        
        //act
        var customers = customerService.GetAllAssets();

        //assert
        customerRepo.Verify(m => m.GetAll(), Times.Once);
        Assert.True(customers.Any());
        Assert.False(customers.Count(c => !c.Active) > 0);

    }
}