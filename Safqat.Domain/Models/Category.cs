using System;
using System.Collections.Generic;

namespace Safqat.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; } = new List<Category>();
        public List<Safqa> Safqas { get; set; } = new List<Safqa>();
    }
}