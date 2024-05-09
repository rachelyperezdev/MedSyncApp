using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRespository
    {
        private readonly ApplicationContext _dbContext;
        public DoctorRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
