using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyExamination : SystemEntity<RadiologyExaminationId>
    {
        public string Name { get; private set; } = default!;
        public RadiologyDevice RadiologyDevice { get; private set; } = default!;
        public decimal Cost { get; private set; }

        public static RadiologyExamination Create(string name, RadiologyDevice radiologyDevice, decimal cost)
        {
            Validation(name, radiologyDevice, cost);

            var examination = new RadiologyExamination
            {
                Id = RadiologyExaminationId.Of(Guid.NewGuid()),
                Name = name,
                RadiologyDevice = radiologyDevice,
                Cost = cost,
            };

            return examination;
        }
        public void Update(string name, RadiologyDevice radiologyDevice, decimal cost)
        {
            Validation(name, radiologyDevice, cost);

            Name = name;
            RadiologyDevice = radiologyDevice;
            Cost = cost;
        }

        private static void Validation(string name, RadiologyDevice radiologyDevice, decimal cost)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            if (!Enum.IsDefined(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }

            ArgumentOutOfRangeException.ThrowIfLessThan(cost, 0);
        }
    }
}
