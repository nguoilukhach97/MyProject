using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class ProductInOrder
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
