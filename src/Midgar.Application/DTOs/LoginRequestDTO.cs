using System.ComponentModel.DataAnnotations;

namespace Midgar.Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}