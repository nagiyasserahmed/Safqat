using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.DTOs;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Auth.Commands.VerifyPasswordResetCode
{
    public class VerifyPasswordResetCodeCommandHandler(IAppDbContext appDbContext)
        : IRequestHandler<VerifyPasswordResetCodeCommand, VerifyPasswordResetCodeResponse>
    {
        public async Task<VerifyPasswordResetCodeResponse> Handle(
            VerifyPasswordResetCodeCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Code))
            {
                return new VerifyPasswordResetCodeResponse
                {
                    Success = false,
                    Message = "Email and code are required."
                };
            }

            var user = await appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return new VerifyPasswordResetCodeResponse
                {
                    Success = false,
                    Message = "Invalid request or code has expired."
                };
            }

            var resetCode = await appDbContext.PasswordResetCodes
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync(c => c.UserId == user.Id, cancellationToken);

            if (resetCode == null || !resetCode.IsValid)
            {
                return new VerifyPasswordResetCodeResponse
                {
                    Success = false,
                    Message = "Code is invalid or has expired."
                };
            }

            bool isCodeValid = resetCode.VerifyCode(request.Code);

            await appDbContext.SaveChangesAsync(cancellationToken);

            if (!isCodeValid)
            {
                if (resetCode.IsMaxAttemptsReached)
                {
                    return new VerifyPasswordResetCodeResponse
                    {
                        Success = false,
                        Message = "Too many failed attempts. Please request a new code."
                    };
                }

                return new VerifyPasswordResetCodeResponse
                {
                    Success = false,
                    Message = "Invalid code. Please try again."
                };
            }

            return new VerifyPasswordResetCodeResponse
            {
                Success = true,
                Message = "Code verified successfully."
            };
        }
    }
}