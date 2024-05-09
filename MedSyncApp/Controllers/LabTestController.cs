using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILabTestService _labTestService;
        private readonly ValidateUserSession _validateUserSession;

        public LabTestController(ILabTestService labTestService, ValidateUserSession validateUserSession)
        {
            _labTestService = labTestService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _labTestService.GetAllViewModel());
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

            SaveLabTestViewModel vm = new();
            return View("SaveLabTest", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveLabTestViewModel vm)
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
                return View("SaveLabTest", vm);    
            }

            await _labTestService.Add(vm);
            return RedirectToRoute(new { controller="LabTest", action="Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("SaveLabTest", await _labTestService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLabTestViewModel vm)
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
                return View("SaveLabTest", vm);
            }

            await _labTestService.Update(vm, vm.Id);
            return RedirectToRoute(new {controller="LabTest",  action="Index"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Administrator)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _labTestService.GetByIdSaveViewModel(id));
        }

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

            await _labTestService.Delete(id);
            return RedirectToRoute(new { controller="LabTest", action="Index" });
        }
    }
}
