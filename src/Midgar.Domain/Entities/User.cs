using System.ComponentModel.DataAnnotations;

namespace Midgar.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(100, ErrorMessage = "The {0} field must not exceed 100 characters.")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}