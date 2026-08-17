using MediatR;
using Safqat.Application.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Auth.Commands.ConfirmPasswordReset
{
    public sealed record ConfirmPasswordResetCommand(string Email, string Code, string NewPassword) : IRequest<ConfirmPasswordResetResponse>;
}
