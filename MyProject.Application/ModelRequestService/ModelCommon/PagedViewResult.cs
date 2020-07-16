using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ModelCommon
{
    public class PagedViewResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
