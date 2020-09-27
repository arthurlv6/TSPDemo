using TSP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TSP.API.Models;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace TSP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptchaController : Controller
    {
        private IHttpClientFactory HttpClientFactory { get; }

        private IOptions<reCAPTCHAVerificationOptions> reCAPTCHAVerificationOptions { get; }

        public CaptchaController(IHttpClientFactory httpClientFactory, IOptions<reCAPTCHAVerificationOptions> reCAPTCHAVerificationOptions)
        {
            this.HttpClientFactory = httpClientFactory;
            this.reCAPTCHAVerificationOptions = reCAPTCHAVerificationOptions;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SampleAPIArgs args)
        {
            var url = "https://www.google.com/recaptcha/api/siteverify";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"secret", this.reCAPTCHAVerificationOptions.Value.Secret},
                {"response", args.reCAPTCHAResponse}
            });

            var httpClient = this.HttpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var verificationResponse = await response.Content.ReadAsAsync<reCAPTCHAVerificationResponse>();
            if (verificationResponse.Success) return Ok();

            return BadRequest(string.Join(", ", verificationResponse.ErrorCodes.Select(err => err.Replace('-', ' '))));
        }
    }
}
