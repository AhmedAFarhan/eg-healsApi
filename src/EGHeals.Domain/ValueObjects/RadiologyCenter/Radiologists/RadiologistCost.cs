namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologistCost
    {
        public decimal Value { get; }
        private RadiologistCost(decimal value) => Value = value;

        public static RadiologistCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new RadiologistCost(value);
        }
    }
}
