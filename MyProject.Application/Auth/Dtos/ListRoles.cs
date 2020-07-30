using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Application.Auth.Dtos
{
    public class ListRoles
    {
        public IList<RoleRequest> Roles { get; set; }
    }
}
