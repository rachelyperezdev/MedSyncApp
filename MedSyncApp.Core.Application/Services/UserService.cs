using AutoMapper;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Enums;

namespace MedSyncApp.Core.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserViewModel _userViewModel;

        public UserService(IUserRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _userRepository.LoginAsync(loginVm);

            if (user == null)
            {
                return null;
            }
            
            UserViewModel userVm = _mapper.Map<UserViewModel>(user);
            return userVm;
        }

        public async Task<bool> UsernameExists(string username)
        {
            var existingUser = await _userRepository.GetByUsername(username);
            return existingUser != null;
        }

        public async Task UpdateByUpdateUserViewModel(UpdateUserViewModel vm)
        {
            SaveUserViewModel saveVm = _mapper.Map<SaveUserViewModel>(vm);
            await Update(saveVm, saveVm.Id); 
        }

        public async Task<UpdateUserViewModel> GetByIdUpdateUserViewModel(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            UpdateUserViewModel vm = _mapper.Map<UpdateUserViewModel>(entity);
            return vm;
        }
    }
}
