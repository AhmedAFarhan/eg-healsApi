namespace EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians
{
    public record RadiologyCenter_TechnicianCost
    {
        public decimal Value { get; }
        private RadiologyCenter_TechnicianCost(decimal value) => Value = value;

        public static RadiologyCenter_TechnicianCost Of(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);

            return new RadiologyCenter_TechnicianCost(value);
        }
    }
}
