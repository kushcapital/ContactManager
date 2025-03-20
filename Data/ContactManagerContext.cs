using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options)
            : base(options)
        {
        }

        public DbSet<ContactManager.Models.Contact> Contacts { get; set; }
        public DbSet<ContactManager.Models.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" }
            );

            // Seed Contacts with hardcoded dates
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Delores",
                    LastName = "Del Rio",
                    Phone = "555-123-4567",
                    Email = "delores@example.com",
                    CategoryId = 1,
                    DateAdded = new DateTime(2025, 3, 5) // 7 days before March 12, 2025
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "555-987-6543",
                    Email = "john@example.com",
                    CategoryId = 2,
                    DateAdded = new DateTime(2025, 3, 9) // 3 days before March 12, 2025
                }
            );
        }
    }
}