using FluentValidation;

namespace dotnet7_new_features.Model
{
    public class RegisterCustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public class Validator : AbstractValidator<RegisterCustomerRequest>
        {
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();    
            }
        }
    }
}
