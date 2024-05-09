using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userService = (IUserService)validationContext.GetService(typeof(IUserService));

            var username = (string)value;
            int currentUserId = 0;

            if (validationContext.ObjectType.Name == "UpdateUserViewModel")
            {
               var model = (UpdateUserViewModel)validationContext.ObjectInstance;
               currentUserId = model.Id;
            }
            else
            {
                var model = (SaveUserViewModel)validationContext.ObjectInstance;
                currentUserId = model.Id;
            }

            if (currentUserId == 0)
            {
                if (userService.UsernameExists(username).GetAwaiter().GetResult())
                {
                    return new ValidationResult(ErrorMessage ?? "Ese nombre de usuario ya está en uso. Por favor, elija otro.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            var currentUser = userService.GetByIdSaveViewModel(currentUserId).GetAwaiter().GetResult();
            var currentUsername = currentUser.Username;

            if (username == currentUsername)
            {
                return ValidationResult.Success;
            }

            if (userService.UsernameExists(username).GetAwaiter().GetResult())
            {
                return new ValidationResult(ErrorMessage ?? "Ese nombre de usuario ya está en uso. Por favor, elija otro.");
            }

            return ValidationResult.Success;
        }
}