using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TSPWebsite.Models;

namespace TSPWebsite.Services
{
    
    public class CaptchaService : BaseService
    {
        private readonly HttpClient _httpClient;
        public CaptchaService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task PostAsync(SampleAPIArgs sampleAPIArgs)
        {
            string url = $"api/captcha";
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(url,sampleAPIArgs);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
