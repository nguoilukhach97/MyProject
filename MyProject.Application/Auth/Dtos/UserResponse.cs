using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.Auth.Dtos
{
    public class UserResponse<T> : ResponseBase
    {
        public T Object { get; set; }
    }
}
