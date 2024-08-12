using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.AuthorizationOperations.RoleRequirement
{
    // Deneyimler menüsü için gerekli olan yetki gereksinimlerini temsil eder.
    public class GenericDynamicRoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Roles { get; set; }

        // Deneyimler menüsü için gerekli olan yetkilerin listesini alır.
        public GenericDynamicRoleRequirement(IEnumerable<string> roles)
        {
            Roles = roles;
        }
    }
}
