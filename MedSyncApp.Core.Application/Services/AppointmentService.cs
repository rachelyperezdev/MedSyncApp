using AutoMapper;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.LabResult;

namespace MedSyncApp.Core.Application.Services
{
    public class AppointmentService : GenericService<SaveAppointmentViewModel, AppointmentViewModel, Appointment>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;
        private readonly ILabResultRepository _labResultRepository;
        private readonly ILabTestRepository _labTestRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILabResultRepository labResultRepository, ILabTestRepository labTestRepository) : base(appointmentRepository, mapper)
        {
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _labResultRepository = labResultRepository;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _labTestRepository = labTestRepository;
        }

        public override async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel vm)
        {
            vm.UserId = _userViewModel.Id;
            vm.Status = AppointmentStatus.PendingConsulting;
            return await base.Add(vm);
        }

        public async Task CompleteAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            appointment.Status = AppointmentStatus.Completed.ToString();

            await _appointmentRepository.UpdateAsync(appointment, appointmentId);
        }

        public async Task<List<LabResultViewModel>> ConsultLabResult(int appointmentId)
        {
            var labResults = await _labResultRepository.GetLabResultsByAppointmentId(appointmentId);

            var PendingAndCompleteLabResults = labResults
                                                    .Where(labResult => labResult.Status == LabResultStatus.Pending.ToString() || labResult.Status == LabResultStatus.Completed.ToString())
                                                    .Select(labResult => new LabResultViewModel
                                                    {
                                                        Id = labResult.Id,
                                                        LabTestName = labResult.LabTest.Name,
                                                        Status = labResult.Status,
                                                        AppointmentId = appointmentId
                                                    })
                                                    .ToList();

            return PendingAndCompleteLabResults;
        }

        public async Task<List<LabTestViewModel>> ConsultPendingConsultation(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment.Status == AppointmentStatus.PendingConsulting.ToString())
            {
                var labTests = await _labTestRepository.GetAllAsync(); 

                var labTestViewModels = labTests.Select(labTest => new LabTestViewModel
                {
                    Id = labTest.Id,
                    Name = labTest.Name
                }).ToList();

                return labTestViewModels;
            }
            else
            {
                return new List<LabTestViewModel>();
            }
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModelWithInclude()
        {
            var appointments = await _appointmentRepository.GetAllWithIncludeAsync(new List<string> { "Patient", "Doctor" });

            return appointments
                    .Where(appointment => appointment.UserId == _userViewModel.Id)
                    .Select(appointment => new AppointmentViewModel
                    {
                        Id = appointment.Id,
                        PatientId = appointment.PatientId,
                        PatientName = $"{appointment.Patient.Firstname} {appointment.Patient.Lastname}",
                        DoctorId = appointment.DoctorId,
                        DoctorName = $"{appointment.Doctor.Firstname} {appointment.Doctor.Lastname}",
                        Date = appointment.Date,
                        Hour = appointment.Hour,
                        Cause = appointment.Cause,
                        Status = Enum.Parse<AppointmentStatus>(appointment.Status)
                    })
                    .ToList();
        }

        public async Task<List<LabResultViewModel>> GetCompletedLabResults(int appointmentId)
        {
            var labResults = await _labResultRepository.GetLabResultsByAppointmentId(appointmentId);

            var completedResults = labResults
                .Where(labResult => labResult.Status == LabResultStatus.Completed.ToString())
                .Select(labResult => new LabResultViewModel
                {
                    Id = labResult.Id,
                    LabTestName = labResult.LabTest.Name,
                    ResultText = labResult.Result,
                    PatientFullname = $"{labResult.Patient.Firstname} {labResult.Patient.Lastname}"
                })
                .ToList();

            return completedResults;
        }

        public async Task PerformLabTests(int appointmentId, List<int> labTestIds)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment.Status == AppointmentStatus.PendingConsulting.ToString())
            {
                foreach (var labTestId in labTestIds)
                {
                    var labResult = new LabResult
                    {
                        LabTestId = labTestId,
                        Status = LabResultStatus.Pending.ToString(),
                        UserId = _userViewModel.Id,
                        AppointmentId = appointmentId,
                        PatientId = appointment.PatientId
                    };

                    await _labResultRepository.AddAsync(labResult);
                }

                appointment.Status = AppointmentStatus.PendingResult.ToString();
                await _appointmentRepository.UpdateAsync(appointment, appointmentId);
            }
        }

        public override async Task Update(SaveAppointmentViewModel vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Update(vm, id);
        }  
    }
}
