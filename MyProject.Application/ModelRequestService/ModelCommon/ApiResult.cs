using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.ModelRequestService.ModelCommon
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }
        public ApiResult(bool isSuccess, string mess)
        {
            IsSuccessed = isSuccess;
            Message = mess;
        }
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
