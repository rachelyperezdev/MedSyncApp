using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ILabTestService _labTestService;

        public AppointmentController(IAppointmentService appointmentService, ValidateUserSession validateUserSession, IPatientService patientService, IDoctorService doctorService, ILabTestService labTestService)
        {
            _appointmentService = appointmentService;
            _validateUserSession = validateUserSession;
            _patientService = patientService;
            _doctorService = doctorService;
            _labTestService = labTestService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if(_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller="Home", action="Index" });
            }

            var appointments = await _appointmentService.GetAllViewModelWithInclude();

            return View(await _appointmentService.GetAllViewModelWithInclude());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveAppointmentViewModel vm = new();
            vm.Patients = await _patientService.GetAllViewModel();
            vm.Doctors = await _doctorService.GetAllViewModel();    

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Patients = await _patientService.GetAllViewModel();
                vm.Doctors = await _doctorService.GetAllViewModel();
                return View(vm);
            }

            await _appointmentService.Add(vm);
            return RedirectToRoute(new { controller="Appointment", action="Index" });
        }

        public async Task<IActionResult> Consult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveAppointmentViewModel vm = await _appointmentService.GetByIdSaveViewModel(id);
            vm.LabTests = await _labTestService.GetAllViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Consult(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if(vm.LabTestIds == null)
            {
                vm.LabTests = await _labTestService.GetAllViewModel();
                return View(vm);
            }

            await _appointmentService.PerformLabTests(vm.Id, vm.LabTestIds!);

            return RedirectToRoute(new { controller="Appointment", action="Index"});
        }

        public async Task<IActionResult> ConsultLabResult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _appointmentService.ConsultLabResult(id));
        }

        public async Task<IActionResult> CompleteAppointment(int appointmentId)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _appointmentService.CompleteAppointment(appointmentId);

            return RedirectToRoute(new {controller = "Appointment", action = "Index"});    
        }

        public async Task<IActionResult> ShowCompletedLabResults(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _appointmentService.GetCompletedLabResults(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _appointmentService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _appointmentService.Delete(id);

            return RedirectToRoute(new { controller="Appointment", action="Index" });
        }
    }
}
