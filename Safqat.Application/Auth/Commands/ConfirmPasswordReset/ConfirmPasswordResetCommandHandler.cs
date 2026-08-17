using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.DTOs;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;


namespace Safqat.Application.Auth.Commands.ConfirmPasswordReset
{
    public class ConfirmPasswordResetCommandHandler(
        IAppDbContext appDbContext,
        IPasswordHasher passwordHasher)
        : IRequestHandler<ConfirmPasswordResetCommand, ConfirmPasswordResetResponse>
    {
        public async Task<ConfirmPasswordResetResponse> Handle(
            ConfirmPasswordResetCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Code) ||
                string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return new ConfirmPasswordResetResponse
                {
                    Success = false,
                    Message = "All fields are required."
                };
            }

            var user = await appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return new ConfirmPasswordResetResponse
                {
                    Success = false,
                    Message = "Invalid request."
                };
            }

            var resetCode = await appDbContext.PasswordResetCodes
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync(c => c.UserId == user.Id, cancellationToken);

            if (resetCode == null || !resetCode.IsValid)
            {
                return new ConfirmPasswordResetResponse
                {
                    Success = false,
                    Message = "Code is invalid or has expired."
                };
            }

            if (!resetCode.VerifyCode(request.Code))
            {
                await appDbContext.SaveChangesAsync(cancellationToken);
                return new ConfirmPasswordResetResponse
                {
                    Success = false,
                    Message = "Invalid code."
                };
            }

            string newPasswordHash = passwordHasher.Hash(request.NewPassword);
            user.UpdatePassword(newPasswordHash);

            resetCode.MarkAsUsed();

            var activeRefreshTokens = await appDbContext.RefreshTokens
                .Where(r => r.UserId == user.Id && !r.IsRevoked && r.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var refreshToken in activeRefreshTokens)
            {
                refreshToken.Revoke();
            }

            await appDbContext.SaveChangesAsync(cancellationToken);

            return new ConfirmPasswordResetResponse
            {
                Success = true,
                Message = "Password has been reset successfully. Please log in with your new password."
            };
        }
    }
}