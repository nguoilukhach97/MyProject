using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Data.Entities
{
    public class Evalute
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public int Rate { get; set; }
        public string ContentEvalute { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
