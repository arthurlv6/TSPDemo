using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TSPWebsite.Models;

namespace TSPWebsite.Services
{
    
    public class SubItemDetailService : BaseService
    {
        private readonly HttpClient _httpClient;
        public SubItemDetailService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<SubItemDetailModel>> GetSubItemDetailAsync(string submenuItemId,int page=1, int size=100, string keyword="", string token="")
        {
            string url = $"api/SubItemDetail?page={page}&size={size}&keyword={keyword}&subMenuItemId={submenuItemId}";
            var httpResponseMessage = await _httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var data = await httpResponseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<SubItemDetailModel>>(data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception(httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
}
