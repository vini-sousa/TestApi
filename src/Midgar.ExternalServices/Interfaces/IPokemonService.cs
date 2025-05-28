using Midgar.ExternalServices.DTOs;
using System.Threading.Tasks;

namespace Midgar.ExternalServices.Interfaces
{
    public interface IPokemonService
    {
        Task<PokemonDTO> GetPokemonAsync(string pokemonName);
    }
}