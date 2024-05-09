using AutoMapper;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MedSyncApp.Core.Application.Services
{
    public class LabTestService : GenericService<SaveLabTestViewModel, LabTestViewModel, LabTest>, ILabTestService
    {
        private readonly ILabTestRepository _labTestRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public LabTestService(ILabTestRepository labTestRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(labTestRepository, mapper)
        {
            _labTestRepository = labTestRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveLabTestViewModel> Add(SaveLabTestViewModel vm)
        {
            vm.UserId = _userViewModel.Id;
            return await base.Add(vm);
        }

        public override async Task Update(SaveLabTestViewModel vm, int id)
        {
            vm.UserId = _userViewModel.Id;
            await base.Update(vm, id);
        }
    }
}
