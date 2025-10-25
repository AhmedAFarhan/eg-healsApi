namespace BuildingBlocks.Domain.Security
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterableAttribute : Attribute
    {
        public bool IsFilterable { get; }
        public string[]? AllowedRoles { get; }

        public FilterableAttribute(bool isFilterable = true, string[]? allowedRoles = null)
        {
            IsFilterable = isFilterable;
            AllowedRoles = allowedRoles ?? new string[0];
        }
    }
}
