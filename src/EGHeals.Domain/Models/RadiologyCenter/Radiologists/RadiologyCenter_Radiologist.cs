using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.RadiologyCenter.Radiologists
{
    public class RadiologyCenter_Radiologist : AuditableAggregate<RadiologyCenter_RadiologistId>
    {
        private readonly List<RadiologyCenter_RadiologistDevice> _devices = new();
        private readonly List<RadiologyCenter_RadiologistExaminationCost> _examinationCosts = new();

        public IReadOnlyList<RadiologyCenter_RadiologistDevice> Devices => _devices.AsReadOnly();
        public IReadOnlyList<RadiologyCenter_RadiologistExaminationCost> ExaminationCosts => _examinationCosts.AsReadOnly();

        public string Name { get; private set; } = default!;
        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;

        public static RadiologyCenter_Radiologist Create(string name, TeamWorkMemberId teamWorkMemberId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            var radiologist = new RadiologyCenter_Radiologist
            {
                Id = RadiologyCenter_RadiologistId.Of(Guid.NewGuid()),
                Name = name,
            };

            return radiologist;
        }

        public void AddDevice(RadiologyCenetr_Device radiologyDevice)
        {
            if (!Enum.IsDefined(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }

            var device = new RadiologyCenter_RadiologistDevice(Id, radiologyDevice);

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

            var examCost = new RadiologyCenter_RadiologistExaminationCost(Id, examinationId, cost);

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
