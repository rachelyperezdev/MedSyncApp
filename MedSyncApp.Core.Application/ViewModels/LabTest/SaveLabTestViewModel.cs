using MedSyncApp.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.LabTest
{
    public class SaveLabTestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la prueba de laboratorio.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
