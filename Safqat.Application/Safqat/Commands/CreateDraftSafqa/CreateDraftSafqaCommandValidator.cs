using FluentValidation;

namespace Safqat.Application.Safqat.Commands.CreateDraftSafqa
{
    public sealed class CreateDraftSafqaCommandValidator: AbstractValidator<CreateDraftSafqaCommand>
    {
        public CreateDraftSafqaCommandValidator()
        {
            RuleFor(x => x.PublisherId).NotEmpty().WithMessage("PublisherId is required.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required.");
            RuleFor(x => x.PublisherId).NotEqual(Guid.Empty).WithMessage("PublisherId cannot be empty.");
            RuleFor(x => x.CategoryId).NotEqual(Guid.Empty).WithMessage("CategoryId cannot be empty.");
        }
    }
}
