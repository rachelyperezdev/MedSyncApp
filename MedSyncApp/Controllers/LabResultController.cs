using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;
using MedSyncApp.Core.Application.ViewModels.LabResult;

namespace MedSyncApp.Controllers
{
    public class LabResultController : Controller
    {
        private readonly ILabResultService _labResultService;
        private readonly ValidateUserSession _validateUserSession;

        public LabResultController(ILabResultService labResultService, ValidateUserSession validateUserSession)
        {
            _labResultService = labResultService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterLabResultViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            ViewBag.PatientIdentificationCard = vm.PatientIdentificationCard;

            return View(await _labResultService.GetAllViewModelWithFilters(vm));
        }

        public async Task<IActionResult> ReportLabResult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _labResultService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> ReportLabResult(SaveLabResultViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            await _labResultService.ReportLabResult(vm.Id, vm.Result);

            return RedirectToRoute(new { controller="LabResult", action="Index" });
            
        }
    }
}
