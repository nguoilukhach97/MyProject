using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyProject.Application;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Common;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json,Encoding.UTF8,"application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response =await client.PostAsync("/Api/User",httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;   

        }

        public async Task<Pagination<UserViewModel>> GetUserPaging(SearchingBase request)
        {
            var client = _httpClientFactory.CreateClient();
            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var session = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",session);

            var response = await client.GetAsync($"/api/user/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");

            var data = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<Pagination<UserViewModel>>(data);

            return users;
        }

        public async Task<ResponseBase> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            
            var response = await client.PostAsync($"/api/user/register",httpContent);

            var data = await response.Content.ReadAsStringAsync();
            var register = JsonConvert.DeserializeObject<ResponseBase>(data);

            return register;
        }

        public async Task<ResponseBase> Delete(Guid id)
        {
            var session = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var response = await client.DeleteAsync($"/api/user/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseBase>(body);

            return JsonConvert.DeserializeObject<ResponseBase>(body);
        }

        public async Task<ResponseBase> UpdateUser(Guid id, UpdateUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var session = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/user/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            

            return JsonConvert.DeserializeObject<ResponseBase>(result);
        }
    }
}
