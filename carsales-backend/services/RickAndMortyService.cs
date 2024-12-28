using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Carsales.Models;
using Microsoft.Extensions.Options;

namespace Carsales.Services
{
    public class RickAndMortyService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public RickAndMortyService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _httpClient = httpClient;
        }

        public async Task<PaginationResult<RickAndMortyObj>> GetEpisodesAsync(int page, int pageSize)
        {
            // Ajustar el número de página para alinearse con la API (inicia en 1)
            string apiUrl = $"{_settings.ApiBaseUrl}/episode?page={page + 1}";
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonDocument.Parse(content);

            // Obtener el total de páginas directamente de la API
            int totalPages = apiResponse.RootElement.GetProperty("info").GetProperty("pages").GetInt32();
            int totalCount = apiResponse.RootElement.GetProperty("info").GetProperty("count").GetInt32();

            // Obtener los episodios para la página solicitada
            var cars = apiResponse.RootElement.GetProperty("results").EnumerateArray()
                .Select(e => new RickAndMortyObj
                {
                    Id = e.GetProperty("id").GetInt32(),
                    Nombre = e.GetProperty("name").GetString(),
                    Fecha = e.GetProperty("air_date").GetString(),
                    Episodio = e.GetProperty("episode").GetString(),
                })
                .ToList();

            return new PaginationResult<RickAndMortyObj>
            {
                TotalCount = totalCount,
                TotalPages = totalPages, // Usamos el valor proporcionado por la API
                Page = page,
                PageSize = pageSize,
                Data = cars
            };
        }
    }
}
