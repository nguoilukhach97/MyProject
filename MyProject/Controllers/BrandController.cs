using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProject.Service;

namespace MyProject.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandApi _brand;

        public BrandController(IBrandApi brand)
        {
            _brand = brand;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetBrands()
        {
            var result = await _brand.GetBrands();
            return Json(result);
        }
    }
}
