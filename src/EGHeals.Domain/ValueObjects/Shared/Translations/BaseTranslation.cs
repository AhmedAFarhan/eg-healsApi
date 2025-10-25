namespace EGHeals.Domain.ValueObjects.Shared.Translations
{
    public class BaseTranslation<TId> : SystemEntity<TId>
    {
        internal BaseTranslation(string name, LanguageCode languageCode)
        {
            Name = name;
            LanguageCode = languageCode;
        }

        public string Name { get; set; } = default!;
        public LanguageCode LanguageCode { get; set; } = LanguageCode.ENGLISH;
    }
}
