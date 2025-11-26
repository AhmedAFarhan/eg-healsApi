using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyCenter_ExaminationPreference : AuditableAggregate<RadiologyCenter_ExaminationPreferenceId>
    {
        private readonly List<RadiologyCenter_ExaminationConsumablePreference> _consumablePreferences = new();

        public IReadOnlyList<RadiologyCenter_ExaminationConsumablePreference> ConsumablePreferences => _consumablePreferences.AsReadOnly();

        public string Name { get; private set; } = default!;
        public RadiologyCenter_ExaminationId ExaminationId { get; private set; } = default!;

        public static RadiologyCenter_ExaminationPreference Create(string name, RadiologyCenter_ExaminationId examinationId)
        {
            Validation(name);

            var examinationPreference = new RadiologyCenter_ExaminationPreference
            {
                Id = RadiologyCenter_ExaminationPreferenceId.Of(Guid.NewGuid()),
                Name = name,
            };

            return examinationPreference;
        }
        public void Update(string name, RadiologyCenter_ExaminationId examinationId)
        {
            Validation(name);

            Name = name;
        }

        public void AddConsumablePreference(RadiologyCenter_ProductId radiologyItemId, decimal qty)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qty);

            var consumable = new RadiologyCenter_ExaminationConsumablePreference(Id, radiologyItemId, qty);

            _consumablePreferences.Add(consumable);
        }
        public void UpdateConsumablePreference(RadiologyCenter_ProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumablePreferences.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumablePreference(RadiologyCenter_ProductId radiologyItemId)
        {
            var consumable = _consumablePreferences.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                _consumablePreferences.Remove(consumable);
            }
        }

        private static void Validation(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);
        }
    }
}
