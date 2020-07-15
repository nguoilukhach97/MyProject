using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsDefault { get; set; }
        public bool Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
