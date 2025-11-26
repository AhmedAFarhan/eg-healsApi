using EGHeals.Domain.Enums.RadiologyCenter;
using EGHeals.Domain.ValueObjects.RadiologyCenter.Technicians;

namespace EGHeals.Domain.Models.RadiologyCenter.Technicians
{
    public class RadiologyCenter_TechnicianDevice : AuditableEntity<RadiologyCenter_TechnicianDeviceId>
    {
        internal RadiologyCenter_TechnicianDevice(RadiologyCenter_TechnicianId technicianId, RadiologyCenetr_Device radiologyDevice)
        {
            Id = RadiologyCenter_TechnicianDeviceId.Of(Guid.NewGuid());
            TechnicianId = technicianId;
            RadiologyDevice = radiologyDevice;
        }

        public RadiologyCenter_TechnicianId TechnicianId { get; private set; } = default!;
        public RadiologyCenetr_Device RadiologyDevice { get; private set; } = default!;
    }
}
