using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class UserRolePermission : Entity<UserRolePermissionId>
    {
        internal UserRolePermission(UserRoleId userRoleId, RolePermissionId rolePermissionId)
        {
            Id = UserRolePermissionId.Of(Guid.NewGuid());
            UserRoleId = userRoleId;
            RolePermissionId = rolePermissionId;
        }

        public UserRoleId UserRoleId { get; private set; } = default!;
        public RolePermissionId RolePermissionId { get; private set; } = default!;
        public RolePermission RolePermission { get; private set; } = default!;/* NAVAIGATION PROPERTY */
    }
}
