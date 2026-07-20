using Safqat.Domain.Enums;

namespace Safqat.Domain.Models
{
    public class SafqaMedia
    {
        public Guid Id { get; set; }
        public Guid SafqaId { get; set; }
        public string Key { get; set; } = string.Empty; // S3 key
        public MediaType Type { get; set; }
        public MediaStatus Status { get; set; } = MediaStatus.Pending;
        public int DisplayOrder { get; set; }
        public bool IsCover { get; set; }
        public long? SizeBytes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Safqa? Safqa { get; set; }

        private SafqaMedia()
        {
        }

        public SafqaMedia(Guid safqaId, string key, MediaType type)
        {
            Id = Guid.NewGuid();
            SafqaId = safqaId;
            Key = key;
            Type = type;
            Status = MediaStatus.Pending;
            DisplayOrder = 0;
            IsCover = false;
            SizeBytes = null;
            CreatedAt = DateTime.UtcNow;
        }


        public void MarkAsUploaded()
        {
            if (Status != MediaStatus.Pending)
                throw new InvalidOperationException("Only pending media can be marked as uploaded.");
            Status = MediaStatus.Uploaded;
        }

        public void MarkAsReady()
        {
            if (Status != MediaStatus.Uploaded)
                throw new InvalidOperationException("Only uploaded media can be marked as ready.");
            Status = MediaStatus.Ready;
        }

        public void MarkAsFailed()
        {
            if (Status != MediaStatus.Pending && Status != MediaStatus.Uploaded)
                throw new InvalidOperationException("Only pending or uploaded media can be marked as failed.");
            Status = MediaStatus.Failed;
        }

        public void SetDisplayOrder(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order), "Display order must be non-negative.");
            DisplayOrder = order;
        }

        public void SetAsCover()
        {
            IsCover = true;
        }


        public void UnsetAsCover()
        {
            IsCover = false;
        }


        public void SetSize(long sizeBytes)
        {
            if (sizeBytes < 0)
                throw new ArgumentOutOfRangeException(nameof(sizeBytes), "Size must be non-negative.");
            SizeBytes = sizeBytes;
        }

        public void ClearSize()
        {
            SizeBytes = null;
        }

        public void UpdateKey(string newKey)
        {
            if (string.IsNullOrWhiteSpace(newKey))
                throw new ArgumentException("Path cannot be null or whitespace.", nameof(newKey));
            Key = newKey;
        }
    }
}