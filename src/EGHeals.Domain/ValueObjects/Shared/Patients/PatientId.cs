namespace EGHeals.Domain.ValueObjects.Shared.Patients
{
    public record PatientId
    {
        public Guid Value { get; }

        private PatientId(Guid value) => Value = value;

        public static PatientId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                //Throw custom inner exception.
                throw new DomainException("PatientId can not be empty");
            }

            return new PatientId(value);
        }
    }
}
