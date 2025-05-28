using Microsoft.AspNetCore.Mvc;
using Midgar.ExternalServices.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Midgar.API.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("The Pokémon name cannot be empty.");

            if (name.All(char.IsDigit)) 
                return BadRequest("Searching for Pokémon by ID is not allowed. Please use the Pokémon name.");

            var pokemonData = await _pokemonService.GetPokemonAsync(name);

            if (pokemonData == null)
                return NotFound($"Pokémon with name ‘{name}’ not found.");

            return Ok(pokemonData);
        }
    }
}