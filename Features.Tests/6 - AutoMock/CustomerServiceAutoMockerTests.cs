using Features.CustomerFolder;
using Features.Tests._4___Dados_Humanos;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace Features.Tests._6___AutoMock;

public class CustomerServiceAutoMockerTests
{
    readonly CustomerTestsBogusFixture _customerTestsBogusFixture;

    public CustomerServiceAutoMockerTests(CustomerTestsBogusFixture customerTestsBogusFixture)
    {
        _customerTestsBogusFixture = customerTestsBogusFixture;
    }

    [Fact(DisplayName = "Add Customer Successfully")]
    [InlineData("Category", "Customer Service AutoMock Tests")]
    public void CustomerService_Add_MustBeExecuteSuccessfully()
    {
        //arrange
        var customer = _customerTestsBogusFixture.GenerateValidCustomer();
        var mocker = new AutoMocker();

        var customerService = mocker.CreateInstance<CustomerService>();
        //act
        customerService.Add(customer);

        //assert
        Assert.True(customer.IsValid()); //isso deve ser validado dentro do metodo Add.
        mocker.GetMock<ICustomerRepository>().Verify(m => m.Add(customer), Times.Once));
        mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Add Customer Failed")]
    [InlineData("Category", "Customer Service AutoMock Tests")]
    public void CustomerService_Add_MustBeExecuteFailed()
    {
        //arrange
        var customer = _customerTestsBogusFixture.GenerateInvalidCustomer();
       var mocker = new AutoMocker();
       var customerService = mocker.CreateInstance<CustomerService>();

        //act
        customerService.Add(customer);

        //assert
        Assert.False(customer.IsValid()); //isso deve ser validado dentro do metodo Add.
        mocker.GetMock<ICustomerRepository>().Verify(m => m.Add(customer), Times.Never);
        mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Get Active Customers")]
    [Trait("Category", "Customer Service AutoMock Tests")]
    public void CustomerService_GetAllActiveCustomers_MustReturnAllActiveCustomers()
    {
        //arrange
        var mocker = new AutoMocker();
        var customerService = mocker.CreateInstance<CustomerService>();
            
        mocker.GetMock<ICustomerRepository>().Setup(c => c.GetAll())
            .Returns(_customerTestsBogusFixture.GetVariedCustomers());

        //act
        var customers = customerService.GetAllAssets();

        //assert
        mocker.GetMock<ICustomerRepository>().Verify(m => m.GetAll(), Times.Once);
        Assert.True(customers.Any());
        Assert.False(customers.Count(c => !c.Active) > 0);
    }
}