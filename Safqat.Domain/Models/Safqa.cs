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
        public static Safqa CreateDraft(Guid id, Guid publisherId, Guid categoryId)
        {
            return new Safqa
            {
                Id = id,
                PublisherId = publisherId,
                CategoryId = categoryId,
                Status = SafqaStatus.Draft
            };
        }

        public void UpdateDraft(string title, string description, string address, decimal price, bool isNegotiable)
        {
            if (price < 0) throw new ArgumentException("Price cannot be negative.");
            Title = title;
            Description = description;
            Address = address;
            Price = price;
            IsNegotiable = isNegotiable;
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