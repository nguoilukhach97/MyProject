using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest;
using MyProject.Application.Service;
using MyProject.Data.Entities;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var param = new ProductPaingParam()
            {
                CategoryId = new List<int> { 1 },
                pageIndex = 1,
                pageSize = 10

            };
            var data = await _service.GetAllPaging(param);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProductCreateRequest request)
        {
            var result = await _service.CreateProduct(request);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody]ProductUpdateRequest request)
        {
            var result = await _service.UpdateProduct(request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteProduct(id);

            return Ok();
        }
    }
}
