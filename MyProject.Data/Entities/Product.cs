using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        
        public string Description { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }
        public int ViewCount { get; set; }

        public bool Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

        public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<Evalute> Evalutes { get; set; }

    }
}
