using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedSyncApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MedSyncApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public LoginController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Home", action="Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UserViewModel userVm = await _userService.Login(loginVm);

            if (userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
            }

            return View(loginVm);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller="Login", action="Index" });
        }
    }
}
