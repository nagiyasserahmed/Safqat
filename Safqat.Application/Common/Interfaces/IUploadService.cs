using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Common.Interfaces
{
    public interface IUploadService
    {
        Task<string> GeneratePreSignedUrlAsync(string fileKey, TimeSpan expirationTime);
    }
}
