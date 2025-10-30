using BuildingBlocks.Domain.Abstractions.Interfaces;
using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.Models.Shared.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.IdentityDomains
{
    public class AppUser : IdentityUser<UserId>, IEntity
    {
        private readonly List<UserPermission> _userPermissions = new();
        private readonly List<UserClientApplication> _userClientApplications = new();

        public IReadOnlyList<UserPermission> UserPermissions => _userPermissions.AsReadOnly();
        public IReadOnlyList<UserClientApplication> UserClientApplications => _userClientApplications.AsReadOnly();

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;      
        public bool IsActive { get; set; } = true;

        /*************************************** System Entity Properties **********************************************/

        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public UserId OwnershipId { get; set; } = default!;

        public void AddPermissionsRange(List<UserPermission> permissions) => _userPermissions.AddRange(permissions);
        public void AddClientAppsRange(List<UserClientApplication> clientApps) => _userClientApplications.AddRange(clientApps);

    }
}
