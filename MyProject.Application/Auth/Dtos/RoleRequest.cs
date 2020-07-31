using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.Auth.Dtos
{
    public class RoleRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
