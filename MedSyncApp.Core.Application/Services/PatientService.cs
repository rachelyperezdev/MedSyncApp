using AutoMapper;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Patient;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MedSyncApp.Core.Application.Helpers;


namespace MedSyncApp.Core.Application.Services
{
    public class PatientService :  GenericService<SavePatientViewModel, PatientViewModel, Patient>, IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public PatientService(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(patientRepository, mapper)
        {
            _patientRepository = patientRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SavePatientViewModel> Add(SavePatientViewModel vm)
        {
            vm.UserId = _userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Update(SavePatientViewModel vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Update(vm, id);
        }
    }
}
