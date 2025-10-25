using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;

namespace EGHeals.Domain.Models.RadiologyCenter.Examinations
{
    public class RadiologyExaminationPreference : Aggregate<RadiologyExaminationPreferenceId>
    {
        private readonly List<RadiologyExaminationConsumablePreference> _consumablePreferences = new();

        public IReadOnlyList<RadiologyExaminationConsumablePreference> ConsumablePreferences => _consumablePreferences.AsReadOnly();

        public string Name { get; private set; } = default!;
        public RadiologyExaminationId ExaminationId { get; private set; } = default!;

        public static RadiologyExaminationPreference Create(string name, RadiologyExaminationId examinationId)
        {
            Validation(name);

            var examinationPreference = new RadiologyExaminationPreference
            {
                Id = RadiologyExaminationPreferenceId.Of(Guid.NewGuid()),
                Name = name,
            };

            return examinationPreference;
        }
        public void Update(string name, RadiologyExaminationId examinationId)
        {
            Validation(name);

            Name = name;
        }

        public void AddConsumablePreference(RadiologyProductId radiologyItemId, decimal qty)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(qty);

            var consumable = new RadiologyExaminationConsumablePreference(Id, radiologyItemId, qty);

            _consumablePreferences.Add(consumable);
        }
        public void UpdateConsumablePreference(RadiologyProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumablePreferences.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumablePreference(RadiologyProductId radiologyItemId)
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
