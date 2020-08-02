using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest.Product
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }

        public string Description { get; set; }
        public string Details { get; set; }
        
        public string UserModified { get; set; }
        

        public bool Status { get; set; }

        public IFormFile ProductImage { get; set; }
    }
}
