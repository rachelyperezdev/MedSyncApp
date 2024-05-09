using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;

namespace MedSyncApp.Core.Application.ViewModels.Doctor
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentificationCard { get; set; }
        public IFormFile? File { get; set; }
        public string? ImageURL { get; set; }
        public ICollection<AppointmentViewModel>? Appointments { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
