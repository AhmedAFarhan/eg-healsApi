using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;
using EGHeals.Domain.ValueObjects.Shared.Stuff;

namespace EGHeals.Domain.Models.RadiologyCenter.Radiologists
{
    public class Radiologist : Aggregate<RadiologistId>
    {
        private readonly List<RadiologistDevice> _devices = new();
        private readonly List<RadiologistExaminationCost> _examinationCosts = new();

        public IReadOnlyList<RadiologistDevice> Devices => _devices.AsReadOnly();
        public IReadOnlyList<RadiologistExaminationCost> ExaminationCosts => _examinationCosts.AsReadOnly();

        public string Name { get; private set; } = default!;
        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;

        public static Radiologist Create(string name, TeamWorkMemberId teamWorkMemberId)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            var radiologist = new Radiologist
            {
                Id = RadiologistId.Of(Guid.NewGuid()),
                Name = name,
            };

            return radiologist;
        }

        public void AddDevice(RadiologyDevice radiologyDevice)
        {
            if (!Enum.IsDefined(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }

            var device = new RadiologistDevice(Id, radiologyDevice);

            _devices.Add(device);
        }
        public void RemoveDevice(RadiologyDevice radiologyDevice)
        {
            var device = _devices.FirstOrDefault(x => x.RadiologyDevice == radiologyDevice);

            if (device is not null)
            {
                _devices.Remove(device);
            }
        }

        public void AddExaminationCost(RadiologyExaminationId examinationId, decimal cost)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(cost);

            var examCost = new RadiologistExaminationCost(Id, examinationId, cost);

            _examinationCosts.Add(examCost);
        }
        public void RemoveExaminationCost(RadiologyExaminationId examinationId)
        {
            var examCost = _examinationCosts.FirstOrDefault(x => x.ExaminationId == examinationId);

            if (examCost is not null)
            {
                _examinationCosts.Remove(examCost);
            }
        }
    }
}
