using Core.Utilities.AuthorizationOperations.RoleRequirement;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.AuthorizationOperations.RoleHandler
{
    public class GenericDynamicRoleHandler : AuthorizationHandler<GenericDynamicRoleRequirement>
    {
        private readonly UserManager<WriterUser> _userManager;
        private readonly ILogger<GenericDynamicRoleHandler> _logger;

        public GenericDynamicRoleHandler(UserManager<WriterUser> userManager, ILogger<GenericDynamicRoleHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GenericDynamicRoleRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (user == null)
            {
                context.Fail();
                return;
            }

            var userRoleList = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            _logger.LogInformation("Kullanıcı Rolleri: {Roles}", string.Join(", ", userRoleList));
            _logger.LogInformation("Gerekli Roller: {Roles}", string.Join(", ", requirement.Roles));

            var userRoles = await _userManager.GetRolesAsync(user);

            if (requirement.Roles.Any(role => userRoles.Contains(role)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
