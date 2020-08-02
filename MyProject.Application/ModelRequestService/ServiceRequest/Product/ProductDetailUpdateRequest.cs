using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ServiceRequest.Product
{
    public class ProductDetailUpdateRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public int Quantity { get; set; }
        public int Warranty { get; set; }
        public int Size { get; set; }

        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public bool Status { get; set; }
    }
}
