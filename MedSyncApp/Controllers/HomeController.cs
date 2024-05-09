using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;
        private readonly ILabTestService _labTestService;
        private readonly ILabResultService _labResultService;
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IPatientService patientService, IDoctorService doctorService, IAppointmentService appointmentService, ILabTestService labTestService, ILabResultService labResultService, IUserService userService, ValidateUserSession validateUserSession)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _labTestService = labTestService;
            _labResultService = labResultService;
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if(_validateUserSession.GetUserRole() == Core.Application.Enums.Role.Administrator)
            {
                return View("IndexAdministrator");
            }
            else
            {
                return View("IndexAssistant");
            }
        }
    }
}
