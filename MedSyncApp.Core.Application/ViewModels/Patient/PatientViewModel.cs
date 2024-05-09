using MedSyncApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;

namespace MedSyncApp.Core.Application.ViewModels.Patient
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string IdentificationCard { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergy { get; set; }
        public IFormFile? File { get; set; }
        public string? ImageURL { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}

