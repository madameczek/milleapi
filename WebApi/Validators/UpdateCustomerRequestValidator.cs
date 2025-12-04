using FluentValidation;
using WebApi.Models.Requests;

namespace WebApi.Validators;

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MinimumLength(2).WithMessage("LastName must be at least 2 characters")
            .MaximumLength(200).WithMessage("LastName must not exceed 200 characters");

        RuleFor(x => x.FirstName)
            .MaximumLength(200).WithMessage("FirstName must not exceed 200 characters")
            .When(x => !string.IsNullOrEmpty(x.FirstName));
    }
}