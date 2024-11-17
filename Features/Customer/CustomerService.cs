using MediatR;

namespace Features.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _mediator;

    public CustomerService(ICustomerRepository customerRepository,
        IMediator mediator)
    {
        _customerRepository = customerRepository;
        _mediator = mediator;
    }

    public IEnumerable<Customer> GetAllAssets()
    {
        return _customerRepository.GetAll().Where(c => c.Active);
    }

    public void Add(Customer customer)
    {
        if (!customer.IsValid())
            return;

        _customerRepository.Add(customer);
        _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "Olá", "Bem vindo!"));
    }

    public void Update(Customer customer)
    {
        if (!customer.IsValid())
            return;

        _customerRepository.Update(customer);
        _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "Mudanças", "Dê uma olhada!"));
    }

    public void Inactivate(Customer cliente)
    {
        if (!cliente.IsValid())
            return;

        cliente.Inactivate();
        _customerRepository.Update(cliente);
        _mediator.Publish(new CustomerEmailNotification("admin@me.com", cliente.Email, "Até breve", "Até mais tarde!"));
    }

    public void Delete(Customer customer)
    {
        _customerRepository.Delete(customer.Id);
        _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "Adeus",
            "Tenha uma boa jornada!"));
    }

    public void Dispose()
    {
        _customerRepository.Dispose();
    }
}