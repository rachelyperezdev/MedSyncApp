using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.Doctor;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.Patient;
using MedSyncApp.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.Appointment
{
    public class SaveAppointmentViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un paciente.")]
        public int? PatientId { get; set; }


        public List<PatientViewModel>? Patients { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un doctor.")]
        public int? DoctorId { get; set; }


        public List<DoctorViewModel>? Doctors { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una o más pruebas de laboratorio.")]
        public List<int>? LabTestIds { get; set; }
        public List<LabTestViewModel>? LabTests { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de la cita.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Debe ingresar la hora de la cita.")]
        [DataType(DataType.Time)]
        public TimeSpan Hour { get; set; }

        [Required(ErrorMessage = "Debe ingresar la causa de la cita.")]
        [DataType(DataType.Text)]
        public string? Cause { get; set; }


        public int UserId { get; set; }
        public UserViewModel? User { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
