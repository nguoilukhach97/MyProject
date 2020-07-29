using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.Auth.Dtos
{
    public class AuthenticateResponse : ResponseBase
    {
        
        public string AccessToken { get; set; }
    }
}
