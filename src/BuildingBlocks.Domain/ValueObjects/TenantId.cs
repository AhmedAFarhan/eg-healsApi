
namespace BuildingBlocks.Domain.ValueObjects
{
    public record TenantId
    {
        public Guid Value { get; }

        private TenantId(Guid value) => Value = value;

        public static TenantId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            return new TenantId(value);
        }
    }
}
