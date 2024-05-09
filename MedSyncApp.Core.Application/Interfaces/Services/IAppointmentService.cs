using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Core.Application.ViewModels.LabResult;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface IAppointmentService : IGenericService<SaveAppointmentViewModel, AppointmentViewModel, Appointment>
    {
        Task CompleteAppointment(int appointmentId);
        Task<List<LabResultViewModel>> ConsultLabResult(int appointmentId);
        Task<List<AppointmentViewModel>> GetAllViewModelWithInclude();
        Task<List<LabResultViewModel>> GetCompletedLabResults(int appointmentId);
        Task PerformLabTests(int appointmentId, List<int> labTestIds);
    }
}
