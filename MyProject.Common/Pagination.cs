using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Common
{
    public class Pagination<T>
    {
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
