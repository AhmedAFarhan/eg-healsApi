using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.RadiologyCenter.Technicians
{
    public class RadiologyCenter_Technician : AuditableEntity<RadiologyCenter_TechnicianId>
    {
        private readonly List<RadiologyCenter_TechnicianDevice> _devices = new();
        private readonly List<RadiologyCenter_TechnicianExaminationCost> _examinationCosts = new();

        public IReadOnlyList<RadiologyCenter_TechnicianDevice> Devices => _devices.AsReadOnly();
        public IReadOnlyList<RadiologyCenter_TechnicianExaminationCost> ExaminationCosts => _examinationCosts.AsReadOnly();

        public string Name { get; private set; } = default!;
        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;

        public static RadiologyCenter_Technician Create(string name, TeamWorkMemberId teamWorkMemberId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            var technician = new RadiologyCenter_Technician
            {
                Id = RadiologyCenter_TechnicianId.Of(Guid.NewGuid()),
                Name = name,
            };

            return technician;
        }

        public void AddDevice(RadiologyCenetr_Device radiologyDevice)
        {
            if (!Enum.IsDefined(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }

            var device = new RadiologyCenter_TechnicianDevice(Id, radiologyDevice);

            _devices.Add(device);
        }
        public void RemoveDevice(RadiologyCenetr_Device radiologyDevice)
        {
            var device = _devices.FirstOrDefault(x => x.RadiologyDevice == radiologyDevice);

            if (device is not null)
            {
                _devices.Remove(device);
            }
        }

        public void AddExaminationCost(RadiologyCenter_ExaminationId examinationId, decimal cost)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(cost);

            var examCost = new RadiologyCenter_TechnicianExaminationCost(Id, examinationId, cost);

            _examinationCosts.Add(examCost);
        }
        public void RemoveExaminationCost(RadiologyCenter_ExaminationId examinationId)
        {
            var examCost = _examinationCosts.FirstOrDefault(x => x.ExaminationId == examinationId);

            if (examCost is not null)
            {
                _examinationCosts.Remove(examCost);
            }
        }
    }
}
