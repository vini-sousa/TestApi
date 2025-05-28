using System.ComponentModel.DataAnnotations;

namespace Midgar.ExternalServices.DTOs
{
    public class PokemonDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; } 
    }
}