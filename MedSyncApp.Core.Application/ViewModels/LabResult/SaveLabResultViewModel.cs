using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.LabResult
{
    public class SaveLabResultViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el resultado de la prueba de laboratorio.")]
        public string Result { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
