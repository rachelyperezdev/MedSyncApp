using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Repositories
{
    public interface ILabResultRepository : IGenericRepository<LabResult>
    {
        Task<List<LabResult>> GetLabResultsByAppointmentId(int appointmentId);
    }
}
