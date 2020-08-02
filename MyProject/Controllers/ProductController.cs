using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.ModelRequestService.ServiceRequest.Product;
using MyProject.Common;
using MyProject.Service;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApi _productApi;
        public ProductController(IProductApi productApi)
        {
            _productApi = productApi;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            var search = new SearchingBase()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var result = await _productApi.GetAllPaging(search);
            
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["result"] = "Thêm sản phẩm thất bại !";
                return RedirectToAction("Index","Product");
            }
            var result = await _productApi.CreateProduct(request);
            if (result.Successed)
            {
                TempData["result"] = "Thêm sản phẩm thành công !";
                return RedirectToAction("Index", "Product");
            }
            TempData["result"] = "Thêm sản phẩm thất bại !";
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<JsonResult> GetProduct(int id)
        {
            var result = await _productApi.GetProduct(id);
            return Json(result);
        }
        
    }
}
