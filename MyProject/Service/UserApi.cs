using Microsoft.Extensions.Configuration;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public class UserApi : IUserApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json,Encoding.UTF8,"application/json");


            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response =await client.PostAsync("/Api/User/Authenticate",httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;   

        }

        public async Task<PagedViewResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request)
        {
            
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",request.BearerToken);
            var response = await client.GetAsync($"/api/user/paging?pageIndex={request.pageIndex}" +
                $"&pageSize={request.pageSize}&keyword={request.Keyword}");

            var data = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PagedViewResult<UserViewModel>>(data);

            return users;
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            
            var response = await client.PostAsync($"/api/user/register",httpContent);

            

            return response.IsSuccessStatusCode;
        }
    }
}
