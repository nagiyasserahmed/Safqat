using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } 
        public string LastName { get; private set; } 
        public string Email { get; private set; } 
        public string Phone { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PasswordHash { get; private set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public object UserName { get; set; }

        public User(string FirstName, string LastName, string Email, string HashedPassword, string Phone, string Country, string City, string Region )
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrEmpty(FirstName))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(FirstName));
            }

            if (string.IsNullOrEmpty(Email)) { 
                throw new ArgumentException("Email cannot be null or empty.", nameof(Email));
            }

            if (string.IsNullOrEmpty(Phone))
            {
                throw new ArgumentException("PhoneNumber cannot be null or empty.", nameof(Phone));
            }

            if (!string.IsNullOrEmpty(Country)) {
                throw new ArgumentException("Location cannot be null or empty.", nameof(Country));
            }

            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Country = Country;
            this.City = City;
            this.Region = Region;
            this.PasswordHash = HashedPassword;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
