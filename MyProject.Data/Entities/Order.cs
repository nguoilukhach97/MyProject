using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public string ReasonCancelation { get; set; }
        public int UserCreated { get; set; }
        public int? UserCancel { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
