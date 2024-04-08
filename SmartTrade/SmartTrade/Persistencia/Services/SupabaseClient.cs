using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Supabase.Core;
using Supabase.Functions;

namespace SmartTrade.Persistencia.Services
{
    public class SupabaseClient
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public SupabaseClient(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataAsync(string table)
        {
            // Construye la URL para hacer la solicitud GET
            string requestUrl = $"{_baseUrl}/table/{table}";

            // Configura la solicitud con la clave de API
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);

            // Realiza la solicitud GET y obtiene la respuesta
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            // Lee y devuelve los datos de la respuesta
            return await response.Content.ReadAsStringAsync();
        }
       

        // Métodos adicionales para realizar otras operaciones como INSERT, UPDATE, DELETE, etc.
    }
}

