using FluentValidation;

namespace Safqat.Application.Safqat.Commands.UpdateDraftSafqa
{
    public class UpdateDraftSafqaCommandValidator: AbstractValidator<UpdateDraftSafqaCommand>
    {
        public UpdateDraftSafqaCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.IsNegotiable).NotEmpty();
        }
    }
}
