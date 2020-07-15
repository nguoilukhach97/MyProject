using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
        public int? SortOrder { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }
    }
}
