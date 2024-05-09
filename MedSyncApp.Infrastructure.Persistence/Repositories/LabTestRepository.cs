using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
    {
        private readonly ApplicationContext _dbContext;
        public LabTestRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
