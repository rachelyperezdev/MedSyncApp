using MedSyncApp.Core.Application.ViewModels.Patient;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface IPatientService : IGenericService<SavePatientViewModel, PatientViewModel, Patient>
    {
    }
}
