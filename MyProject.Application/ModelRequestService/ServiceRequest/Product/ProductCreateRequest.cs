using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest.Product
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        
        public string Description { get; set; }
        public string Details { get; set; }
        
        public string UserCreated { get; set; }
        

        public bool Status { get; set; }

        public IFormFile ImageProduct { get; set; }
    }
}
