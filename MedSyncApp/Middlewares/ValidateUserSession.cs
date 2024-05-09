using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Enums;

namespace MedSyncApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if(userViewModel == null)
            {
                return false;
            }

            return true;
        }

        public Role GetUserRole()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            return userViewModel.Role;
        }


    }
}
