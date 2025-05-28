using System.Net.Http.Json;
using Midgar.ExternalServices.DTOs;
using Midgar.ExternalServices.Interfaces;

namespace Midgar.ExternalServices.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<PokemonDTO> GetPokemonAsync(string pokemonName)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
                return null;

            try
            {                
                var response = await _httpClient.GetAsync($"pokemon/{pokemonName.ToLower()}");

                response.EnsureSuccessStatusCode();

                var pokemon = await response.Content.ReadFromJsonAsync<PokemonDTO>();
                
                return pokemon;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error obtaining Pokémon data: {ex.Message}");

                return null;
            }
        }
    }
}