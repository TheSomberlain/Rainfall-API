using Newtonsoft.Json;
using RainfallAPI.Interfaces;
using RainfallAPI.Models.InputModels;
using System.Collections.Generic;
using System.Net.Http;

namespace RainfallAPI.Services
{
    public class RainfallDataService : IRainfallDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RainfallDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ReadingItem>> GetRainfallDataAsync(string stationId, int count)
        {
            if (string.IsNullOrWhiteSpace(stationId))
            {
                throw new ArgumentException("Station ID cannot be null or empty.", nameof(stationId));
            }

            string requestUri = String.Format("flood-monitoring/id/stations/{0}/readings.json?parameter=rainfall&_limit={1}", stationId, count);

            try
            {
                HttpClient client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://environment.data.gov.uk/");
                HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var monitoringResponse = JsonConvert.DeserializeObject<FloodMonitoringResponse>(content);
                return monitoringResponse.Items;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Invalid request to the API.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occured in API response.", ex);
            }
        }
    }
 }

