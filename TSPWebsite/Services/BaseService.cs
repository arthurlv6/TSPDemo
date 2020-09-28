using Microsoft.AspNetCore.Components;
using TSPWebsite.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TSPWebsite.Services
{
    
    public class BaseService
    {
        protected readonly HttpClient httpClient;
        public BaseService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
    }
}
