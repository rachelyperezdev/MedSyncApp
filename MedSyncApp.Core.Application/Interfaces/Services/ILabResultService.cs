using MedSyncApp.Core.Application.ViewModels.LabResult;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface ILabResultService : IGenericService<SaveLabResultViewModel, LabResultViewModel, LabResultViewModel>
    {
        Task<List<LabResultViewModel>> GetAllViewModelWithInclude();
        Task<List<LabResultViewModel>> GetAllViewModelWithFilters(FilterLabResultViewModel filters);
        Task ReportLabResult(int labResultId, string result);
    }
}
