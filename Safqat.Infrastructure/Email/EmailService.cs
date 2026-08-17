using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Safqat.Application.Common.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Safqat.Infrastructure.Email
{
    public class EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        public async Task SendPasswordResetCodeAsync(string email, string code, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                    Subject = "Password Reset Code",
                    Body = code,
                    IsBodyHtml = true
                };

                message.To.Add(email);

                using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(message);
                logger.LogInformation("Email sent successfully to {To}", email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email to {To}", email);
                throw;
            }
        }
    }
}
