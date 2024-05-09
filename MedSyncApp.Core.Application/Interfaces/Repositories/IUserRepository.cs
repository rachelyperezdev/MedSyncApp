using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> LoginAsync(LoginViewModel loginVm);
    }
}
