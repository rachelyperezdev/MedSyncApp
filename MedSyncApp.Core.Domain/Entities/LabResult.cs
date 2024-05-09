using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class LabResult : AuditableBaseEntity
    {
        public string? Result { get; set; }
        public string Status { get; set; }
        public int? LabTestId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int UserId { get; set; }

        public LabTest? LabTest { get; set; }
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
        public User? User { get; set; }
    }
}
