using Microsoft.EntityFrameworkCore;
using ContactManager.Models;

namespace ContactManager.Data
{
    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friend" },
                new Category { CategoryId = 3, Name = "Work" }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "123-456-7890",
                    Email = "john.doe@example.com",
                    CategoryId = 1,
                    DateAdded = DateTime.Now.AddDays(-7)
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Phone = "098-765-4321",
                    Email = "jane.smith@example.com",
                    CategoryId = 2,
                    DateAdded = DateTime.Now.AddDays(-3)
                }
            );
        }
    }
}