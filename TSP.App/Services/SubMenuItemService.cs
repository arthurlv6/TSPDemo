using TSP.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TSP.App.Services
{
    
    public class SubMenuItemService : BaseService
    {
        private readonly HttpClient _httpClient;
        public SubMenuItemService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IList<M>> GetAll<M>(int subSystemId, string token = null) where M : BaseModel
        {
            try
            {
                if (!httpClient.DefaultRequestHeaders.Contains("Authorization") && !string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                string url = @"api/submenuitem/"+subSystemId;
                var data = await httpClient.GetStreamAsync(url);
                return await JsonSerializer.DeserializeAsync<IList<M>>
                    (data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
