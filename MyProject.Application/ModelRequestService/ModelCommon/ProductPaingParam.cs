using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ModelCommon
{
    public class ProductPaingParam : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> CategoryId { get; set; }
    }
}
