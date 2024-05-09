using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<bool> UsernameExists(string username);
        Task<UpdateUserViewModel> GetByIdUpdateUserViewModel(int id);
        Task UpdateByUpdateUserViewModel(UpdateUserViewModel vm);
    }
}
