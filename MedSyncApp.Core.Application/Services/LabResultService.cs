using AutoMapper;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.LabResult;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Enums;

namespace MedSyncApp.Core.Application.Services
{
    public class LabResultService : GenericService<SaveLabResultViewModel, LabResultViewModel, LabResult>, ILabResultService
    {
        private readonly ILabResultRepository _labResultRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public LabResultService(ILabResultRepository labResultRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(labResultRepository, mapper)
        {
            _labResultRepository = labResultRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveLabResultViewModel> Add(SaveLabResultViewModel vm)
        {
            vm.UserId = _userViewModel.Id;
            return await base.Add(vm);
        }

        public async Task<List<LabResultViewModel>> GetAllViewModelWithFilters(FilterLabResultViewModel filters)
        {
            var labResultList = await _labResultRepository.GetAllWithIncludeAsync(new List<string> { "LabTest", "Patient" });

            var filteredList = labResultList
                                .Where(labResult => labResult.Status == LabResultStatus.Pending.ToString())
                                .Select(labResult => new LabResultViewModel
                                {
                                    Id = labResult.Id,
                                    PatientFullname = $"{labResult.Patient.Firstname} {labResult.Patient.Lastname}",
                                    PatientIdentificationCard = labResult.Patient.IdentificationCard,
                                    LabTestName = labResult.LabTest.Name,
                                    Status = labResult.Status
                                });

            if (!string.IsNullOrEmpty(filters.PatientIdentificationCard))
            {
                filteredList = filteredList
                    .Where(labResult => labResult.PatientIdentificationCard == filters.PatientIdentificationCard);
            }

            return filteredList.ToList();
        }

        public async Task<List<LabResultViewModel>> GetAllViewModelWithInclude()
        {
            var labResultList = await _labResultRepository.GetAllWithIncludeAsync(new List<string> { "LabTest", "Patient" });

            return labResultList
                    .Where(labResult => labResult.UserId == _userViewModel.Id && labResult.Status == LabResultStatus.Pending.ToString())
                    .Select(labResult => new LabResultViewModel
                    {
                        PatientFullname = $"{labResult.Patient.Firstname} {labResult.Patient.Lastname}",
                        PatientIdentificationCard = labResult.Patient.IdentificationCard,
                        LabTestName = labResult.LabTest.Name,
                        Status = labResult.Status
                    }).ToList();
        }

        public async Task ReportLabResult(int labResultId, string result)
        {
            var labResult = await _labResultRepository.GetByIdAsync(labResultId);

            if (labResult != null)
            {
                labResult.Result = result;
                labResult.Status = LabResultStatus.Completed.ToString();

                await _labResultRepository.UpdateAsync(labResult, labResultId);
            }
        }

        public override async Task Update(SaveLabResultViewModel vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Update(vm, id);
        }
    }
}
