using EGHeals.Domain.Models.Shared.Translations;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class RoleTranslation : BaseTranslation<RoleTranslationId>
    {
        internal RoleTranslation(RoleId roleId, string name, LanguageCode languageCode) : base(name, languageCode)
        {
            Id = RoleTranslationId.Of(Guid.NewGuid());
            RoleId = roleId;
        }

        public RoleId RoleId { get; set; } = default!;
    }
}
