using AutoMapper;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.Doctor;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MedSyncApp.Core.Application.Helpers;

namespace MedSyncApp.Core.Application.Services
{
    public class DoctorService : GenericService<SaveDoctorViewModel, DoctorViewModel, Doctor>, IDoctorService
    {
        private readonly IDoctorRespository _doctorRespository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public DoctorService(IDoctorRespository doctorRespository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(doctorRespository, mapper)
        {
            _doctorRespository = doctorRespository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveDoctorViewModel> Add(SaveDoctorViewModel vm)
        {
            vm.UserId = _userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Update(SaveDoctorViewModel vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Update(vm, id);
        }
    }
}
