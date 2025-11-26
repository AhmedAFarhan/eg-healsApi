using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class Role : AuditableAggregate<RoleId>
    {
        private readonly List<RolePermission> _rolePermissions = new();
        public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        /***************************************** Domain Business *****************************************/

        public static Role Create(RoleId id, string name, bool isActive = true)
        {
            //Domain model validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Role name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "Role name should be in range between 3 and 150 characters.");
            }

            var role = new Role
            {
                Id = id,
                Name = name,
                IsActive = isActive
            };

            return role;
        }

        public RolePermission AddPermission(PermissionId permissionId)
        {
            var rolePermission = new RolePermission(Id, permissionId);

            _rolePermissions.Add(rolePermission);

            return rolePermission;
        }
        public void RemovePermission(PermissionId permissionId)
        {
            var rolePermission = _rolePermissions.FirstOrDefault(x => x.PermissionId == permissionId);

            if (rolePermission is not null)
            {
                _rolePermissions.Remove(rolePermission);
            }
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("Role is already activated");
            }

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new DomainException("Role is already deactivated");
            }

            IsActive = false;
        }
    }
}
