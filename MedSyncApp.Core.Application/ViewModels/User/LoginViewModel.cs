using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su nombre de usuario.")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar su contraseña.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
