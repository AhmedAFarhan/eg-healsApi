using EGHeals.Domain.Enums;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class Role : SystemAggregate<RoleId>
    {
        private readonly List<RolePermission> _permissions = new();
        private readonly List<RoleTranslation> _translations = new();

        public IReadOnlyList<RolePermission> Permissions => _permissions.AsReadOnly();
        public IReadOnlyList<RoleTranslation> Translations => _translations.AsReadOnly();

        public string Name { get; private set; } = default!;
        public UserActivity? UserActivity { get; private set; } // if ActivityType == null → global (system-level)
        public bool IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } = true;

        /***************************************** Domain Business *****************************************/

        public static Role Create(RoleId id, string name, UserActivity? userActivity, bool isAdmin, bool isActive = true)
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

            if(userActivity.HasValue && !Enum.IsDefined(typeof(UserActivity), userActivity.Value))
            {
                throw new ArgumentException("user activity value is out of range.", nameof(userActivity));
            }

            var role = new Role
            {
                Id = id,
                Name = name,
                UserActivity = userActivity,
                IsAdmin = isAdmin,
                IsActive = isActive
            };

            return role;
        }

        public void AddPermission(PermissionId permissionId)
        {
            var permission = new RolePermission(Id, permissionId);

            _permissions.Add(permission);
        }
        public void RemovePermission(PermissionId permissionId)
        {
            var permission = _permissions.FirstOrDefault(x => x.PermissionId == permissionId);

            if (permission is not null)
            {
                _permissions.Remove(permission);
            }
        }

        public void AddTranslation(string name, LanguageCode languageCode)
        {
            //Domain model validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("translation name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "translation name should be in range between 3 and 150 characters.");
            }

            if (!Enum.IsDefined(languageCode))
            {
                throw new ArgumentException("language code value is out of range.", nameof(languageCode));
            }

            var translation = new RoleTranslation(Id, name, languageCode);

            _translations.Add(translation);
        }
        public void RemoveTranslation(LanguageCode languageCode)
        {
            var translation = _translations.FirstOrDefault(x => x.LanguageCode == languageCode);

            if (translation is not null)
            {
                _translations.Remove(translation);
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
