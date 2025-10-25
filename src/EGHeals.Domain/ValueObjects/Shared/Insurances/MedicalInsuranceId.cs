namespace EGHeals.Domain.ValueObjects.Shared.Insurances
{
    public record MedicalInsuranceId
    {
        public Guid Value { get; }

        private MedicalInsuranceId(Guid value) => Value = value;

        public static MedicalInsuranceId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("MedicalInsuranceId can not be empty");
            }

            return new MedicalInsuranceId(value);
        }
        public static MedicalInsuranceId? OfNullable(Guid? value)
        {
            if (value is null)
            {
                return null;
            }

            if (value == Guid.Empty)
            {
                //Throw custom exception
                throw new DomainException("MedicalInsuranceId can not be empty");
            }

            return new MedicalInsuranceId(value.Value);
        }
    }
}
