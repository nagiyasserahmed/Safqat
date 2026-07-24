using Safqat.Domain.Enums;

namespace Safqat.Domain.Models
{
    public class Safqa
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsNegotiable { get; set; }
        public SafqaStatus Status { get; set; } = SafqaStatus.Draft;
        public int ItemsQuantity { get; set; } = 1;
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid PublisherId { get; set; }
        public Guid CategoryId { get; set; }
        public User? Publisher { get; set; }
        public Category? Category { get; set; }
        public ICollection<SafqaMedia> Media { get; set; } = new List<SafqaMedia>();

        // Draft created immediately when the user starts a listing —
        // no title/price required yet
        public static Safqa CreateDraft(Guid id ,Guid publisherId, Guid categoryId)
        {
            return new Safqa
            {
                Id = id,
                CategoryId = categoryId,
                PublisherId = publisherId,
                Status = SafqaStatus.Draft
            };
        }

        public void UpdateDraft(
                    string? title = null,
                    string? description = null,
                    string? address = null,
                    decimal? price = null,
                    bool? isNegotiable = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;

            if (!string.IsNullOrWhiteSpace(description))
                Description = description;

            if (!string.IsNullOrWhiteSpace(address))
                Address = address;

            if (price.HasValue)
            {
                if (price.Value < 0)
                    throw new ArgumentException("Price cannot be negative.");

                Price = price.Value;
            }

            if (isNegotiable.HasValue)
                IsNegotiable = isNegotiable.Value;

            UpdatedAt = DateTime.UtcNow;
        }

        public void Publish()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new InvalidOperationException("Title is required to publish.");
            if (Price <= 0)
                throw new InvalidOperationException("Price must be set to publish.");
            if (!Media.Any(m => m.Status == MediaStatus.Ready))
                throw new InvalidOperationException("At least one ready media item is required to publish.");

            Status = SafqaStatus.Active;
            PublishedAt = DateTime.UtcNow;
        }
    }
}