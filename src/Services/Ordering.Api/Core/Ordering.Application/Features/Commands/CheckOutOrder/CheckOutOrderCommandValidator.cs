using FluentValidation;


namespace Ordering.Application.Features.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
    {
        public CheckOutOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.TotalPrice)
                .NotEmpty().WithMessage("Total price is required.")
                .GreaterThan(0).WithMessage("Total price must be greater than zero.");
        }
    }

}
