using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class ProductInCategory
    {
        public int ProductId { get; set; }
        public int CategoyId{ get; set; }

        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
