using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class RolePermission : SystemEntity<RolePermissionId>
    {
        internal RolePermission(RoleId roleId, PermissionId permissionId, bool isActive = true)
        {
            Id = RolePermissionId.Of(Guid.NewGuid());
            RoleId = roleId;
            PermissionId = permissionId;
            IsActive = isActive;
        }

        public RoleId RoleId { get; private set; } = default!;
        public PermissionId PermissionId { get; private set; } = default!;
        public Permission Permission { get; private set; } = default!;/* NAVAIGATION PROPERTY */
        public bool IsActive { get; set; } = true;
    }
}
