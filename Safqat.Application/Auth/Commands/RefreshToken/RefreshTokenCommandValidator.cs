using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandValidator
        : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
