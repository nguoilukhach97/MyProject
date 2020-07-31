using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.Auth.Dtos
{
    public class ValidateResponse
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}
