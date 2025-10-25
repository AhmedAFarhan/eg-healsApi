using BuildingBlocks.DataAccessAbstraction.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EGHeals.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var userIdSt = User?.FindFirstValue("UserId");
                Guid.TryParse(userIdSt, out Guid userId);
                return userId;
            }
        }

        public string? Username => User?.FindFirstValue("UserName");

        public Guid? OwnershipId
        {
            get
            {
                var ownershipIdSt = User?.FindFirstValue("OwnershipId");
                Guid.TryParse(ownershipIdSt, out Guid ownershipId);
                return ownershipId;
            }
        }

        public IEnumerable<string> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
