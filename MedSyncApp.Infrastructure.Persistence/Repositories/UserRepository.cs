using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.Helpers;
using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User user)
        {
            user.Password = PasswordEncryptation.ComputeSha256Hash(user.Password);
            await base.AddAsync(user);
            return user;
        }

        public override async Task<List<User>> GetAllAsync()
        {
            List<User> users = await base.GetAllAsync();
            var filteredUsers = users
                                .Where(u => u.Role == Role.Administrator.ToString() || u.Role == Role.Assistant.ToString()).ToList();
            return filteredUsers;
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string encryptedPassword = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == encryptedPassword);
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(user => user.Username == username);
        }
    }
}
