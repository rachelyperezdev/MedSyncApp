using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Doctor;
using MedSyncApp.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace MedSyncApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ValidateUserSession _validateUserSession;

        public DoctorController(IDoctorService doctorService, ValidateUserSession validateUserSession)
        {
            _doctorService = doctorService;
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

            return View(await _doctorService.GetAllViewModel());
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

            SaveDoctorViewModel vm = new();
            return View("SaveDoctor", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveDoctorViewModel vm)
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
                return View("SaveDoctor", vm);
            }

            SaveDoctorViewModel doctorVm = await _doctorService.Add(vm);

            if (doctorVm.Id != 0 && doctorVm != null)
            {
                doctorVm.ImageURL = UploadFile(vm.File, doctorVm.Id);

                await _doctorService.Update(doctorVm, doctorVm.Id);
            }

            return RedirectToRoute(new { controller="Doctor", action="Index" });
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

            SaveDoctorViewModel vm = await _doctorService.GetByIdSaveViewModel(id);
            return View("SaveDoctor", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel vm)
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
                return View("SaveDoctor", vm);
            }

            SaveDoctorViewModel doctorVm = await _doctorService.GetByIdSaveViewModel(vm.Id);
            vm.ImageURL = UploadFile(vm.File, vm.Id, true, doctorVm.ImageURL);
            await _doctorService.Update(vm, vm.Id);

            return RedirectToRoute(new { controller="Doctor", action="Index" });
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

            return View(await _doctorService.GetByIdSaveViewModel(id));
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

            await _doctorService.Delete(id);

            string basePath = $"/Images/Doctors/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller="Doctor", action="Index"});
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath =" ")
        {
            if (isEditMode)
            {
                if(file == null)
                {
                    return imagePath;
                }
            }

            string basePath = $"/Images/Doctors/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if(!Directory.Exists(path))
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
                string[] oldImagePart = imagePath.Split('/');
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
