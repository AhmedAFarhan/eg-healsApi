using EGHeals.Domain.Models.Shared.Translations;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class PermissionTranslation : BaseTranslation<PermissionTranslationId>
    {
        internal PermissionTranslation(PermissionId permissionId, string name, LanguageCode languageCode) : base(name, languageCode)
        {
            Id = PermissionTranslationId.Of(Guid.NewGuid());
            PermissionId = permissionId;
        }

        public PermissionId PermissionId { get; set; } = default!;
    }
}
