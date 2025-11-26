namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists
{
    public record RadiologyCenter_RadiologistCost
    {
        public decimal Value { get; }
        private RadiologyCenter_RadiologistCost(decimal value) => Value = value;

        public static RadiologyCenter_RadiologistCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new RadiologyCenter_RadiologistCost(value);
        }
    }
}
