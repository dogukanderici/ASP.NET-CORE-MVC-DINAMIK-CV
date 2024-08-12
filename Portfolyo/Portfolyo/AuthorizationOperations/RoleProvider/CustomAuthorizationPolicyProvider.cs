using Business.Abstract;
using Core.Utilities.AuthorizationOperations.RoleRequirement;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Portfolyo.AuthorizationOperations.RoleProvider
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        const string policyPrefix = "Dynamic";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }
        private IPanelRoleService _panelRoleService;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IPanelRoleService panelRoleService)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
            _panelRoleService = panelRoleService;
        }

        public async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(policyPrefix, StringComparison.OrdinalIgnoreCase))
            {

                string[] policyNameList = policyName.Split("-");

                IEnumerable<string> roles = _panelRoleService.TGetListForRoleName(r => r.PanelName == policyNameList[1]);

                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new GenericDynamicRoleRequirement(roles));
                return await Task.FromResult(policy.Build());
            }

            return await FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() =>
            FallbackPolicyProvider.GetFallbackPolicyAsync();
    }
}