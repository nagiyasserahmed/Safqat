using FluentValidation;

namespace Safqat.Application.SafqatMedia.Commands
{
    public sealed class CreateSafqaMediaCommandValidator: AbstractValidator<CreateSafqaMediaCommand>
    {
        public CreateSafqaMediaCommandValidator()
        {
            RuleFor(x => x.SafqaId).NotEmpty().WithMessage("SafqaId is required.");
            RuleFor(x => x.Key).NotEmpty().WithMessage("Key is required.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Type must be a valid MediaType.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status must be a valid MediaStatus.");
        }
    }
}
