using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface ILabTestService : IGenericService<SaveLabTestViewModel, LabTestViewModel, LabTest>
    {
    }
}
