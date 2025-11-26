using EGHeals.Domain.ValueObjects.Shared.Insurances;

namespace EGHeals.Domain.Models.Shared.Insurances
{
    public class MedicalInsurance : AuditableEntity<MedicalInsuranceId>
    {
        public string Name { get; private set; } = default!;

        public static MedicalInsurance Create(string name)
        {
            Validation(name);

            var medicalInsurance = new MedicalInsurance
            {
                Id = MedicalInsuranceId.Of(Guid.NewGuid()),
                Name = name,
            };

            return medicalInsurance;
        }
        public void Update(string name)
        {
            Validation(name);

            Name = name;
        }

        private static void Validation(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 2);
        }
    }
}
