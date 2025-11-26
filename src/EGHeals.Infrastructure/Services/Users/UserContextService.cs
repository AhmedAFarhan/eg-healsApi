using BuildingBlocks.DataAccessAbstraction.Services;
using EGHeals.Domain.Enums.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EGHeals.Infrastructure.Services.Users
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid UserId
        {
            get
            {
                var userIdVal = User?.FindFirstValue("UserId");
                Guid.TryParse(userIdVal, out Guid userId);
                return userId;
            }
        }

        public Guid TenantId
        {
            get
            {
                var tenantIdVal = User?.FindFirstValue("OwnershipId");
                Guid.TryParse(tenantIdVal, out Guid tenantId);
                return tenantId;
            }
        }

        public string Activity
        {
            get
            {
                var userActivity = User?.FindFirstValue("UserActivity");
                return userActivity ?? string.Empty;
            }
        }

        public bool IsSystemUser
        {
            get
            {
                var userActivity = User?.FindFirstValue("UserActivity");
                var userActivityType = (UserActivity)Enum.Parse(typeof(UserActivity), userActivity);
                return userActivityType == UserActivity.SYSTEM;
            }
        }

        public IEnumerable<string> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
