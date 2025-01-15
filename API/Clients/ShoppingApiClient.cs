using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.Extensions.Logging;

namespace Api.Clients
{
    public class ShoppingApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShoppingApiClient> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ShoppingApiClient(HttpClient httpClient, ILogger<ShoppingApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Plasează o comanda catre endpoint-ul API.
        /// </summary>
        public async Task<OrderModel?> PlaceOrderAsync(ProcessOrderCommand orderCommand)
        {
            try
            {
                // Trimiterea comenzii catre endpoint-ul de plasare a comenzilor
                var response = await _httpClient.PostAsJsonAsync("api/orders/place-order", orderCommand, _jsonOptions);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Comanda a fost plasata cu succes.");
                    return await response.Content.ReadFromJsonAsync<OrderModel>(_jsonOptions);
                }

                //  Gestionarea erorilor de raspuns
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Eroare la plasarea comenzii. Status Code: {response.StatusCode}, Error: {error}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Eroare la conexiunea HTTP: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Eroare neasteptata: {ex.Message}");
                return null;
            }
        }
    }
}
