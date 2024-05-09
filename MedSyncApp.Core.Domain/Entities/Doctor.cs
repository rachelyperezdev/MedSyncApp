using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class Doctor : Person
    {
        public string Email { get; set; }
        public int UserId { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public User? User { get; set; }
    }
}
