using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Safqat.Commands.PublishSafqa
{
    public sealed class PublishSafqaCommandValidator: AbstractValidator<PublishSafqaCommand>
    {
        public PublishSafqaCommandValidator()
        {
            RuleFor(x=> x.Id).NotEmpty();
        }
    }
}
