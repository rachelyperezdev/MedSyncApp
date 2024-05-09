using MedSyncApp.Core.Application.ViewModels.Doctor;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Interfaces.Services
{
    public interface IDoctorService : IGenericService<SaveDoctorViewModel, DoctorViewModel, Doctor>
    {
    }
}
