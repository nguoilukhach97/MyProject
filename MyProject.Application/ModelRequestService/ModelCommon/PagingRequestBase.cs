using MyProject.Application.Auth.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ModelCommon
{
    public class PagingRequestBase : RequestBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
