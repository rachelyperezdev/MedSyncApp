using MedSyncApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.Doctor
{
    public class SaveDoctorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar su nombre.")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido.")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Debe ingresar su email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar su número telefónico.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debe ingresar su cédula.")]
        [DataType(DataType.Text)]
        public string IdentificationCard { get; set; }

        public string? ImageURL { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
