using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyProject.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public class BrandApi : IBrandApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BrandApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<List<Brand>> GetBrands()
        {
            var client = _httpClientFactory.CreateClient();
            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var session = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.GetAsync($"/api/brand/getall");

            var data = await response.Content.ReadAsStringAsync();
            var brands = JsonConvert.DeserializeObject<List<Brand>>(data);

            return brands;
        }
    }
}
