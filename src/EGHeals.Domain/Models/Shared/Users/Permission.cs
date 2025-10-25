using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class Permission : SystemAggregate<PermissionId>
    {
        private readonly List<PermissionTranslation> _translations = new();
        public IReadOnlyList<PermissionTranslation> Translations => _translations.AsReadOnly();

        public string Name { get; set; } = default!;

        /***************************************** Domain Business *****************************************/

        public static Permission Create(PermissionId id, string name)
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

            var permission = new Permission
            {
                Id = id,
                Name = name,
            };

            return permission;
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
