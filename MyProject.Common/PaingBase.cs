using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Common
{
    public class PaingBase
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRows / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
