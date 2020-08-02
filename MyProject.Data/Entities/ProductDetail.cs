using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public int Quantity { get; set; }
        public int Warranty { get; set; }
        public int Size { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public bool Status { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }

    }
}
