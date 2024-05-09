using MedSyncApp.Core.Application.Interfaces.Services;
using MedSyncApp.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MedSyncApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #region Services
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<ILabResultService, LabResultService>();
            services.AddTransient<ILabTestService, LabTestService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
