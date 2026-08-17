using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Infrastructure.Email
{
    public class EmailSettings
    {
        public static string SectionName => "EmailSettings";
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
