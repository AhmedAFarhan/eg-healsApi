using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class Permission
    {
        private readonly List<PermissionTranslation> _translations = new();
        public IReadOnlyList<PermissionTranslation> Translations => _translations.AsReadOnly();

        public PermissionId Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public UserActivity UserActivity { get; set; } = UserActivity.SYSTEM;
        public bool IsActive { get; set; } = true;
        public bool IsAdmin { get; set; } = false;

        /***************************************** Domain Business *****************************************/

        public static Permission Create(PermissionId id, string name, UserActivity userActivity, bool isAdmin = false)
        {
            //Domain model validation

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Permission name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "Permission name should be in range between 3 and 150 characters.");
            }

            if (!Enum.IsDefined(typeof(UserActivity), userActivity))
            {
                throw new ArgumentException("UserActivity is out of range.", nameof(userActivity));
            }

            var permission = new Permission
            {
                Id = id,
                Name = name,
                UserActivity = userActivity,
                IsAdmin = isAdmin
            };

            return permission;
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("Permission is already activated");
            }

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new DomainException("Permission is already deactivated");
            }

            IsActive = false;
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

            var translation = new PermissionTranslation(Id, name, languageCode);

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
    }
}
