using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TSPWebsite.Models;

namespace TSPWebsite.Services
{
    
    public class ContactUsService : BaseService
    {
        private readonly HttpClient _httpClient;
        public ContactUsService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task PostAsync(ContactUsModel model)
        {
            var jsonData = JsonSerializer.Serialize(model);
            var modelJson =
                new StringContent(jsonData, Encoding.UTF8, "application/json");
            string url = $"api/contactus";
            var httpResponseMessage = await _httpClient.PostAsync(url, modelJson);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
