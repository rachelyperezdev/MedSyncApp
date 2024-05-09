using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class Patient : Person
    {
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergy { get; set; }
        public int UserId { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<LabResult>? LabResults { get; set; }
        public User? User { get; set; }
    }
}
