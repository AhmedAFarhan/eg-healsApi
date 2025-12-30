using BuildingBlocks.Domain.Abstractions.Interfaces;
using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.IdentityDomains
{
    public class AppUser : IdentityUser<UserId>, IAuditableEntity
    {
        private readonly List<UserRole> _userRoles = new();
        private readonly List<UserClientApplication> _userClientApplications = new();

        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyList<UserClientApplication> UserClientApplications => _userClientApplications.AsReadOnly();

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;      
        public bool IsActive { get; set; } = true;

        /*************************************** Auditable Entity Properties **********************************************/

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public TenantId TenantId { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;

        public void AddRolesRange(List<UserRole> roles)
        {
            // Remove permissions that are not in the domain user
            _userRoles.RemoveAll(r => !roles.Any(ur => ur.RoleId == r.RoleId));

            // Prepare only new permissions that don’t exist yet
            var newPermissions = roles.Where(ur => !_userRoles.Any(r => r.RoleId == ur.RoleId)).ToList();

            _userRoles.AddRange(newPermissions);
        }
        public void AddClientAppsRange(List<UserClientApplication> clientApps) => _userClientApplications.AddRange(clientApps);

    }
}
