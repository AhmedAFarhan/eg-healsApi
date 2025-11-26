using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Radiologists;

namespace EGHeals.Domain.Models.RadiologyCenter.Radiologists
{
    public class RadiologyCenter_RadiologistDevice : AuditableEntity<RadiologyCenter_RadiologistDeviceId>
    {
        internal RadiologyCenter_RadiologistDevice(RadiologyCenter_RadiologistId radiologistId, RadiologyCenetr_Device radiologyDevice)
        {
            Id = RadiologyCenter_RadiologistDeviceId.Of(Guid.NewGuid());
            RadiologistId = radiologistId;
            RadiologyDevice = radiologyDevice;
        }

        public RadiologyCenter_RadiologistId RadiologistId { get; private set; } = default!;
        public RadiologyCenetr_Device RadiologyDevice { get; private set; } = default!;
    }
}
