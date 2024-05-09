using MedSyncApp.Core.Application.Interfaces.Repositories;
using MedSyncApp.Infrastructure.Persistence.Contexts;
using MedSyncApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedSyncApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IDoctorRespository, DoctorRepository>();
            services.AddTransient<ILabResultRepository, LabResultRepository>();
            services.AddTransient<ILabTestRepository, LabTestRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
