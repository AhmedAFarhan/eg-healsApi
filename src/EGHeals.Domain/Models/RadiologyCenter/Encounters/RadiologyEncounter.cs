using EGHeals.Domain.ValueObjects.RadiologyCenter.Encounters;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Examinations;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Products;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians;
using EGHeals.Domain.ValueObjects.Shared.Insurances;
using EGHeals.Domain.ValueObjects.Shared.Patients;
using EGHeals.Domain.ValueObjects.Shared.ReferralDoctors;

namespace EGHeals.Domain.Models.RadiologyCenter.Encounters
{
    public class RadiologyEncounter : Aggregate<RadiologyEncounterId>
    {
        private readonly List<RadiologyEncounterConsumable> _consumables = new();
        public IReadOnlyList<RadiologyEncounterConsumable> Consumables => _consumables.AsReadOnly();

        public PatientId PatientId { get; private set; } = default!;
        public ReferralDoctorId ReferralDoctorId { get; private set; } = default!;
        public RadiologistId RadiologistId { get; private set; } = default!;
        public TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyExaminationId ExaminationId { get; private set; } = default!;
        public RadiologyDevice RadiologyDevice { get; private set; } = default!;
        public EncounterStatus EncounterStatus { get; private set; } = default!;        
        public RadiologyEncounterPricing EncounterPricing { get; private set; } = default!;
        public MedicalInsuranceId? MedicalInsuranceId { get; private set; } = default!;

        public static RadiologyEncounter Create(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologistId radiologistId, TechnicianId technicianId, RadiologyExaminationId examinationId, RadiologyDevice radiologyDevice, EncounterStatus encounterStatus, RadiologyEncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
        {
            //Domain model validation
            Validation(radiologyDevice, encounterStatus);

            var encounter = new RadiologyEncounter
            {
                Id = RadiologyEncounterId.Of(Guid.NewGuid()),
                PatientId = patientId,
                ReferralDoctorId = referralDoctorId,
                RadiologistId = radiologistId,
                TechnicianId = technicianId,
                ExaminationId = examinationId,
                RadiologyDevice = radiologyDevice,
                EncounterStatus = encounterStatus,
                EncounterPricing = encounterPricing,
                MedicalInsuranceId = medicalInsuranceId,
            };

            return encounter;
        }
        public void Update(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologistId radiologistId, TechnicianId technicianId, RadiologyExaminationId examinationId, RadiologyDevice radiologyDevice, EncounterStatus encounterStatus, RadiologyEncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
        {
            //Domain model validation
            Validation(radiologyDevice, encounterStatus);

            PatientId = patientId;
            ReferralDoctorId = referralDoctorId;
            RadiologistId = radiologistId;
            TechnicianId = technicianId;
            ExaminationId = examinationId;
            RadiologyDevice = radiologyDevice;
            EncounterStatus = encounterStatus;
            EncounterPricing = encounterPricing;
            MedicalInsuranceId = medicalInsuranceId;
        }
        
        public void AddConsumable(RadiologyProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = new RadiologyEncounterConsumable(Id, radiologyItemId, quantity);

            _consumables.Add(consumable);
        }
        public void UpdateConsumable(RadiologyProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumable(RadiologyProductId radiologyItemId)
        {
            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                _consumables.Remove(consumable);
            }
        }

        private static void Validation(RadiologyDevice radiologyDevice, EncounterStatus encounterStatus)
        {
            if (!Enum.IsDefined(radiologyDevice))
            {
                throw new DomainException("radiologyDevice value is out of range");
            }
            if (!Enum.IsDefined(encounterStatus))
            {
                throw new DomainException("encounterStatus value is out of range");
            }
        }
    }
}
