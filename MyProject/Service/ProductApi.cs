using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyProject.Common;
using MyProject.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyProject.Application.ModelRequestService.ServiceRequest.Product;
using MyProject.Application;
using MyProject.Application.ModelRequestService.PublicRequest;

namespace MyProject.Service
{
    public class ProductApi : IProductApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApi(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> CreateProduct(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> CreateProductDetail(ProductDetailCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> DeleteProductDetail(int idDetail)
        {
            throw new NotImplementedException();
        }

       
        public Task<Pagination<ProductDetail>> GetAllSize(int id, SearchingBase request)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageViewModel> GetImageById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<Product>> GetAllPaging(SearchingBase request)
        {
            var client = _httpClientFactory.CreateClient();
            //var session = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var session = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);

            var response = await client.GetAsync($"/api/product?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&keyword={request.Keyword}");

            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Pagination<Product>>(data);

            return product;
        }

        public Task<int> RemoveImage(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> UpdateProduct(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> UpdateProductDetail(ProductDetailUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateViewCount(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
