using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        public string? Organization { get; set; } // Optional

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public DateTime DateAdded { get; set; }

        public Category Category { get; set; } // Navigation property

        public string Slug => $"{FirstName}-{LastName}".ToLower().Replace(" ", "-");
    }
}