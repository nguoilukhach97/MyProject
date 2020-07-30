using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Common
{
    public class SearchingBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }
}
