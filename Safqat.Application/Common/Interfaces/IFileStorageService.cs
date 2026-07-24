using Safqat.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        Task<PresignedUploadResult> GenerateUploadUrlAsync(
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default);

        Task<PresignedDownloadResult> GenerateDownloadUrlAsync(
            string key,
            CancellationToken cancellationToken = default);

        Task DeleteFileAsync(
            string key,
            CancellationToken cancellationToken = default);
    }
}
