using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        
        public string Description { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public int ViewCount { get; set; }

        public bool Status { get; set; }
    }
}
