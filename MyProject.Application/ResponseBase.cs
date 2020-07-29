using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application
{
    public class ResponseBase
    {
        public bool Successed { get; set; }
        public Error Errors { get; set; }
    }
}
