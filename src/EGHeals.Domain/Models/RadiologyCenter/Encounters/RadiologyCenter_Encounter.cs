using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.Enums.Shared;
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
    public class RadiologyCenter_Encounter : AuditableAggregate<RadiologyCenter_EncounterId>
    {
        private readonly List<RadiologyCenter_EncounterConsumable> _consumables = new();
        public IReadOnlyList<RadiologyCenter_EncounterConsumable> Consumables => _consumables.AsReadOnly();

        public PatientId PatientId { get; private set; } = default!;
        public ReferralDoctorId ReferralDoctorId { get; private set; } = default!;
        public RadiologyCenter_RadiologistId RadiologistId { get; private set; } = default!;
        public RadiologyCenter_TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyCenter_ExaminationId ExaminationId { get; private set; } = default!;
        public RadiologyCenetr_Device RadiologyDevice { get; private set; } = default!;
        public EncounterStatus EncounterStatus { get; private set; } = default!;        
        public RadiologyCenter_EncounterPricing EncounterPricing { get; private set; } = default!;
        public MedicalInsuranceId? MedicalInsuranceId { get; private set; } = default!;

        public static RadiologyCenter_Encounter Create(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologyCenter_RadiologistId radiologistId, RadiologyCenter_TechnicianId technicianId, RadiologyCenter_ExaminationId examinationId, RadiologyCenetr_Device radiologyDevice, EncounterStatus encounterStatus, RadiologyCenter_EncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
        {
            //Domain model validation
            Validation(radiologyDevice, encounterStatus);

            var encounter = new RadiologyCenter_Encounter
            {
                Id = RadiologyCenter_EncounterId.Of(Guid.NewGuid()),
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
        public void Update(PatientId patientId, ReferralDoctorId referralDoctorId, RadiologyCenter_RadiologistId radiologistId, RadiologyCenter_TechnicianId technicianId, RadiologyCenter_ExaminationId examinationId, RadiologyCenetr_Device radiologyDevice, EncounterStatus encounterStatus, RadiologyCenter_EncounterPricing encounterPricing, MedicalInsuranceId? medicalInsuranceId)
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
        
        public void AddConsumable(RadiologyCenter_ProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = new RadiologyCenter_EncounterConsumable(Id, radiologyItemId, quantity);

            _consumables.Add(consumable);
        }
        public void UpdateConsumable(RadiologyCenter_ProductId radiologyItemId, decimal quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                consumable.Update(quantity);
            }
        }
        public void RemoveConsumable(RadiologyCenter_ProductId radiologyItemId)
        {
            var consumable = _consumables.FirstOrDefault(x => x.RadiologyItemId == radiologyItemId);

            if (consumable is not null)
            {
                _consumables.Remove(consumable);
            }
        }

        private static void Validation(RadiologyCenetr_Device radiologyDevice, EncounterStatus encounterStatus)
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
