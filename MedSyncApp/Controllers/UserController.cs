using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if(_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller="Home", action= "Index" });
            }

            return View(await _userService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveUserViewModel vm = new();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _userService.Add(vm);
            return RedirectToRoute(new { controller="User", action="Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UpdateUserViewModel vm = await _userService.GetByIdUpdateUserViewModel(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var existingUser = await _userService.GetByIdUpdateUserViewModel(vm.Id);
            vm.Role = existingUser.Role;

            if (string.IsNullOrWhiteSpace(vm.Password))
            {
                vm.Password = existingUser.Password;
            }
            else
            {
                vm.Password = PasswordEncryptation.ComputeSha256Hash(vm.Password);
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _userService.UpdateByUpdateUserViewModel(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Delete(id);

            return RedirectToRoute(new { controller="User",  action="Index" });   
        }
    }
}
