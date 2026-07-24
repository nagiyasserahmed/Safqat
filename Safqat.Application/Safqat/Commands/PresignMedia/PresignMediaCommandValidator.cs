using FluentValidation;
using Safqat.Application.Safqat.Commands.PresignMedia;

public sealed class PresignMediaCommandValidator
    : AbstractValidator<PresignMediaCommand>
{
    public PresignMediaCommandValidator()
    {
        RuleFor(x => x.SafqaId)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .Must(x => x.StartsWith("image/"))
            .WithMessage("Only image files are allowed.");
    }
}