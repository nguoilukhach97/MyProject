using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.Product;
using MyProject.Application.Service;
using MyProject.Common;
using MyProject.Data.Entities;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] SearchingBase request)
        {
            var result = await _service.GetAllByCategory(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailAsync(int id,[FromQuery] SearchingBase request)
        {
            var result = await _service.GetAllSize(id, request);
            return Ok(result);
        }
        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetDetailsAsync(int id)
        {
            var result = await _service.GetListImages(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] ProductCreateRequest request)
        {
            var result = await _service.CreateProduct(request);
            return Ok(result);
        }

        [HttpPost("details")]
        public async Task<IActionResult> PostAsync([FromForm]ProductDetailCreateRequest request)
        {
            var result = await _service.CreateProductDetail(request);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] ProductUpdateRequest request)
        {
            var result = await _service.UpdateProduct(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteProduct(id);
            return Ok(result);
        }
    }
}
