using EGHeals.Domain.Enums.Shared;

namespace EGHeals.Domain.Models.Shared.Translations
{
    public class BaseTranslation<TId>
    {
        public TId Id { get; set; } = default!;

        internal BaseTranslation(string name, LanguageCode languageCode)
        {
            Name = name;
            LanguageCode = languageCode;
        }

        public string Name { get; set; } = default!;
        public LanguageCode LanguageCode { get; set; } = LanguageCode.ENGLISH;
    }
}
