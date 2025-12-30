using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyCenter_Examination : BaseAuditableEntity<RadiologyCenter_ExaminationId>
    {
        public string Name { get; private set; } = default!;
        public RadiologyCenetr_Device Device { get; private set; } = default!;
        public decimal Cost { get; private set; }

        public static RadiologyCenter_Examination Create(string name, RadiologyCenetr_Device device, decimal cost)
        {
            Validation(name, device, cost);

            var examination = new RadiologyCenter_Examination
            {
                Id = RadiologyCenter_ExaminationId.Of(Guid.NewGuid()),
                Name = name,
                Device = device,
                Cost = cost,
            };

            return examination;
        }
        public void Update(string name, RadiologyCenetr_Device device, decimal cost)
        {
            Validation(name, device, cost);

            Name = name;
            Device = device;
            Cost = cost;
        }

        private static void Validation(string name, RadiologyCenetr_Device device, decimal cost)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Examination name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "Examination name should be in range between 3 and 150 characters.");
            }

            if (!Enum.IsDefined(device))
            {
                throw new DomainException("Radiology device value is out of range");
            }

            if (cost < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cost), cost, "Examination cost should not be less than zero.");
            }

        }
    }
}
