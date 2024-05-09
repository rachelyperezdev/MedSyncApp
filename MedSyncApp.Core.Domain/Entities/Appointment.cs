using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class Appointment : AuditableBaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public string Cause { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int UserId { get; set; }

        public Patient? Patient { get; set; } 
        public Doctor? Doctor { get; set; } 
        public ICollection<LabResult>? LabResults { get; set; }
        public User? User { get; set; }
    }
}
