using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.DTOs;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Auth.Commands.PasswordReset
{
    public class PasswordResetCommandHandler(IAppDbContext appDbContext, IEmailService emailService)
        : IRequestHandler<PasswordResetCommand, PasswordResetResponse>
    {
        public async Task<PasswordResetResponse> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            var response = new PasswordResetResponse
            {
                Success = true,
                Message = "If an account with this email exists, a reset code has been sent."
            };

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return response;
            }

            var user = await appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return response;
            }

            var activeCodes = await appDbContext.PasswordResetCodes
                .Where(c => c.UserId == user.Id && !c.IsUsed && c.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var activeCode in activeCodes)
            {
                activeCode.Invalidate();
            }

            string generatedCode = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

            var resetCodeEntity = new PasswordResetCode(user.Id, generatedCode, TimeSpan.FromMinutes(10));
            appDbContext.PasswordResetCodes.Add(resetCodeEntity);

            await appDbContext.SaveChangesAsync(cancellationToken);

            await emailService.SendPasswordResetCodeAsync(user.Email, generatedCode, cancellationToken);

            return response;
        }
    }
}