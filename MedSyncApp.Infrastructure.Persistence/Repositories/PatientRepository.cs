using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationContext _dbContext;
        public PatientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
