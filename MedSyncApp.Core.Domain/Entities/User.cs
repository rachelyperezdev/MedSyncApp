using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<LabTest>? LabTests { get; set; }
        public ICollection<LabResult>? LabResults { get; set; }
    }
}
