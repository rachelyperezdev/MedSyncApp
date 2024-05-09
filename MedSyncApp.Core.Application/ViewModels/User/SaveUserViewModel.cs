using MedSyncApp.Core.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace MedSyncApp.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar su nombre de usuario.")]
        [DataType(DataType.Text)]
        [UniqueUsername(ErrorMessage = "Ese nombre de usuario ya está en uso. Por favor, elija otro.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben de coincidir.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Debe ingresar su nombre.")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Debe ingresar su apellido.")]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }


        [Required(ErrorMessage = "Debe ingresar su email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de usuario.")]
        public Role? Role { get; set; }
    }
}
