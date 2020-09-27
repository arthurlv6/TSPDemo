using TSP.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TSP.App.Services
{
    
    public class SubSystemService : BaseService
    {
        private readonly HttpClient _httpClient;
        public SubSystemService(HttpClient httpClient):base(httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
