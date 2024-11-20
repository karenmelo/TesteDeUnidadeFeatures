using Features.Core;
using FluentValidation;

namespace Features.CustomerFolder;

public class Customer: Core.Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public DateTime Created { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }

        protected Customer()
        {
        }

        public Customer(Guid id, string name, string lastName, DateTime birthdate, string email, bool active,
            DateTime created)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Birthdate = birthdate;
            Email = email;
            Active = active;
            Created = created;
        }

        public string Fullname()
        {
            return $"{Name} {LastName}";
        }

        public bool IsSpecial()
        {
            return Created < DateTime.Now.AddYears(-3) && Active;
        }

        public void Inactivate()
        {
            Active = false;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o nome")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o sobrenome")
                .Length(2, 150).WithMessage("O Sobrenome deve ter entre 2 e 150 caracteres");

            RuleFor(c => c.Birthdate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("O cliente deve ter 18 anos ou mais");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
}