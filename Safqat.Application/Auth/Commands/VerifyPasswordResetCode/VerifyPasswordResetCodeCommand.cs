using System;
using System.Collections.Generic;
using MediatR;
using Safqat.Application.Common.DTOs;
using System.Text;

namespace Safqat.Application.Auth.Commands.VerifyPasswordResetCode
{
    public sealed record VerifyPasswordResetCodeCommand(string Email, string Code) : IRequest<VerifyPasswordResetCodeResponse>;
}
