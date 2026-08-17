namespace Safqat.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetCodeAsync(string email, string code, CancellationToken cancellationToken = default);
    }
}