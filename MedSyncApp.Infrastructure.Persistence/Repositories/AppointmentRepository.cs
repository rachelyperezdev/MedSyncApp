using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationContext _dbContext;
        public AppointmentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
