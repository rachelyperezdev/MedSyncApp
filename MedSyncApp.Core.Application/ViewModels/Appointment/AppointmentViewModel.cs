using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.User;

namespace MedSyncApp.Core.Application.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public string Cause { get; set; }
        public AppointmentStatus Status { get; set; }
        public List<LabTestViewModel> LabTests { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
