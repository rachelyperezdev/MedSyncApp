using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Core.Domain.Entities;
using MedSyncApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MedSyncApp.Infrastructure.Persistence.Repositories
{
    public class LabResultRepository : GenericRepository<LabResult>, ILabResultRepository
    {
        private readonly ApplicationContext _dbContext;
        public LabResultRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LabResult>> GetLabResultsByAppointmentId(int appointmentId)
        {
            return await _dbContext.Set<LabResult>()
                .Where(labResult => labResult.AppointmentId == appointmentId) 
                .Select(labResult => new LabResult
                {
                    Id = labResult.Id,
                    AppointmentId = labResult.AppointmentId,
                    LabTest = labResult.LabTest,
                    Status = labResult.Status,
                    Result = labResult.Result,
                    Patient = labResult.Patient
                })  
                .ToListAsync();
        }
    }
}
