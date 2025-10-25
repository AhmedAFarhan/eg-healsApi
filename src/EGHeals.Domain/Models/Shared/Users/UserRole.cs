using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class UserRole : Entity<UserRoleId>
    {
        private readonly List<UserRolePermission> _userRolePermissions = new();
        public IReadOnlyList<UserRolePermission> UserRolePermissions => _userRolePermissions.AsReadOnly();

        internal UserRole(SystemUserId systemUserId, RoleId roleId)
        {
            Id = UserRoleId.Of(Guid.NewGuid());
            SystemUserId = systemUserId;
            RoleId = roleId;
        }

        public SystemUserId SystemUserId { get; private set; } = default!;
        public RoleId RoleId { get; private set; } = default!;

        public Role Role { get; private set; } = default!; /* NAVAIGATION PROPERTY */

        public UserRolePermission AddPermission(RolePermissionId rolePermissionId)
        {
            var userRolePermission = new UserRolePermission(Id, rolePermissionId);

            _userRolePermissions.Add(userRolePermission);

            return userRolePermission;
        }
        public void RemovePermission(RolePermissionId rolePermissionId)
        {
            var userRolePermission = _userRolePermissions.FirstOrDefault(x => x.RolePermissionId == rolePermissionId);

            if (userRolePermission is not null)
            {
                _userRolePermissions.Remove(userRolePermission);
            }
        }
    }
}
