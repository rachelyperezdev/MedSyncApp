using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Patient;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ValidateUserSession _validateUserSession;

        public PatientController(IPatientService patientService, ValidateUserSession validateUserSession)
        {
            _patientService = patientService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller="Login", action="Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(await _patientService.GetAllViewModel());
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

            return View("SavePatient", new SavePatientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel vm)
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
                return View("SavePatient", vm);
            }

            SavePatientViewModel patientVm = await _patientService.Add(vm);

            if (patientVm.Id != 0 && patientVm != null)
            {
                patientVm.ImageURL = UploadFile(vm.File, patientVm.Id);
                await _patientService.Update(patientVm, patientVm.Id);
            }

            return RedirectToRoute(new { controller="Patient", action="Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_validateUserSession.GetUserRole() != Role.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View("SavePatient", await _patientService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel vm)
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
                return View("SavePatient", vm);
            }

            SavePatientViewModel patientVm = await _patientService.GetByIdSaveViewModel(vm.Id);
            vm.ImageURL = UploadFile(vm.File, vm.Id, true, patientVm.ImageURL);
            await _patientService.Update(vm, vm.Id);

            return RedirectToRoute(new { controller="Patient", action="Index" });
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

            return View(await _patientService.GetByIdSaveViewModel(id));
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

            await _patientService.Delete(id);
            return RedirectToRoute(new { controller="Patient", action="Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
