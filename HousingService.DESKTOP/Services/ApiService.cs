using HousingService.DESKTOP.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HousingService.DESKTOP.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration configuration)
        {
            string baseUrl = configuration["ApiSettings:BaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException("ApiSettings:BaseUrl not found in configuration.");
            }
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<CitiesDTO>?> GetCitiesAsync()
        {
            var response = await _httpClient.GetAsync("/cities");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CitiesDTO>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<StreetsDTO>?> GetStreetsByCityAsync(int cityId)
        {
            var response = await _httpClient.GetAsync($"/cities/{cityId}/streets");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<StreetsDTO>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<HousesDTO>?> GetHousesByCityAsync(int cityId)
        {
            var response = await _httpClient.GetAsync($"/cities/{cityId}/houses");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<HousesDTO>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<HousesDTO>?> GetHousesByStreetAsync(int streetId)
        {
            var response = await _httpClient.GetAsync($"/streets/{streetId}/houses");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<HousesDTO>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
