using Bogus;
using Bogus.DataSets;
using Features.CustomerFolder;

namespace Features.Tests._4___Dados_Humanos;


    [CollectionDefinition(nameof(CustomerBogusCollection))]
    public class CustomerBogusCollection : ICollectionFixture<CustomerTestsBogusFixture>
    {}

    public class CustomerTestsBogusFixture : IDisposable
    {
        public Customer GenerateValidCustomer()
        {
            return GenerateCustomers(1, true).FirstOrDefault();
        }

        public IEnumerable<Customer> GetVariedCustomers()
        {
            var customers = new List<Customer>();

            customers.AddRange(GenerateCustomers(50, true).ToList());
            customers.AddRange(GenerateCustomers(50, false).ToList());

            return customers;
        }

        public IEnumerable<Customer> GenerateCustomers(int quantity, bool active)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            //var email = new Faker().Internet.Email("eduardo","pires","gmail");
            //var clientefaker = new Faker<Cliente>();
            //clientefaker.RuleFor(c => c.Nome, (f, c) => f.Name.FirstName());

            var customers = new Faker<Customer>("pt_BR")
                .CustomInstantiator(f => new Customer(
                    Guid.NewGuid(), 
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80,DateTime.Now.AddYears(-18)),
                    "",
                    active,
                    DateTime.Now))
                .RuleFor(c=>c.Email, (f,c) => 
                    f.Internet.Email(c.Name.ToLower(), c.LastName.ToLower()));

            return customers.Generate(quantity);
        }

        public Customer GenerateInvalidCustomer()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Customer>("pt_BR")
                .CustomInstantiator(f => new Customer(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    "",
                    false,
                    DateTime.Now));

            return cliente;
        }

        public void Dispose()
        {
        }
}