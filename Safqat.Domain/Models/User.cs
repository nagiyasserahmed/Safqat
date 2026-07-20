using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } 
        public string Email { get; private set; } 
        public string PhoneNumber { get; private set; }
        public string Location { get; private set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public User(string Name, string Email, string PhoneNumber, string Location)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(Name));
            }

            if (string.IsNullOrEmpty(Email)) { 
                throw new ArgumentException("Email cannot be null or empty.", nameof(Email));
            }

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                throw new ArgumentException("PhoneNumber cannot be null or empty.", nameof(PhoneNumber));
            }

            if (!string.IsNullOrEmpty(Location)) {
                throw new ArgumentException("Location cannot be null or empty.", nameof(Location));
            }

            this.Name = Name;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Location = Location;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
