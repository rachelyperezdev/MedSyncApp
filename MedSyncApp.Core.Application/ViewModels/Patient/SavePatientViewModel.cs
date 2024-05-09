using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.Patient
{
    public class SavePatientViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Debe ingresar su nombre.")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido.")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Debe ingresar su número telefónico.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debe ingresar su dirección.")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Debe ingresar su cédula.")]
        [DataType(DataType.Text)]
        public string IdentificationCard { get; set; }

        [Required(ErrorMessage = "Debe ingresar su fecha de nacimiento.")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Debe indicar si es fumador o no.")]
        public bool IsSmoker { get; set; }

        [Required(ErrorMessage = "Debe indicar si tiene alergias o no.")]
        public bool HasAllergy { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public string? ImageURL { get; set; }

        public int UserId { get; set; }
        public UserViewModel? User { get; set; }

        public List<AppointmentViewModel>? Appointments { get; set; }
        public List<LabTestViewModel>? LabTests { get; set; }
    }
}