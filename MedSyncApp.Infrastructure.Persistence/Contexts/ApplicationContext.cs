using MedSyncApp.Core.Domain.Common;
using MedSyncApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedSyncApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        DbSet<User> Users { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<LabTest> LabTests { get; set; }
        DbSet<LabResult> LabResults { get; set; }
        DbSet<Appointment> Appointments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifyBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<LabTest>().ToTable("LabTests");
            modelBuilder.Entity<LabResult>().ToTable("LabResults");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Doctor>().HasKey(doctor => doctor.Id);
            modelBuilder.Entity<Patient>().HasKey(patient => patient.Id);
            modelBuilder.Entity<LabTest>().HasKey(labTest => labTest.Id);
            modelBuilder.Entity<LabResult>().HasKey(labResult => labResult.Id);
            modelBuilder.Entity<Appointment>().HasKey(appointment => appointment.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<User>()
                        .HasMany<Patient>(user => user.Patients)
                        .WithOne(patient => patient.User)
                        .HasForeignKey(patient => patient.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                        .HasMany<Appointment>(patient => patient.Appointments)
                        .WithOne(appointment => appointment.Patient)
                        .HasForeignKey(appointment => appointment.PatientId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                       .HasMany<LabResult>(patient => patient.LabResults)
                       .WithOne(labTest => labTest.Patient)
                       .HasForeignKey(labTest => labTest.PatientId)
                       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                        .HasMany<Doctor>(user => user.Doctors)
                        .WithOne(doctor => doctor.User)
                        .HasForeignKey(doctor => doctor.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                        .HasMany<Appointment>(doctor => doctor.Appointments)
                        .WithOne(appointment => appointment.Doctor)
                        .HasForeignKey(appointment => appointment.DoctorId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                        .HasMany<LabTest>(user => user.LabTests)
                        .WithOne(labTest => labTest.User)
                        .HasForeignKey(labTest => labTest.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabTest>()
                        .HasMany<LabResult>(labTest => labTest.LabResults)
                        .WithOne(labResult => labResult.LabTest)
                        .HasForeignKey(labResult => labResult.LabTestId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                        .HasMany<LabResult>(user => user.LabResults)
                        .WithOne(labResult => labResult.User)
                        .HasForeignKey(labResult => labResult.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                        .HasMany<Appointment>(user => user.Appointments)
                        .WithOne(appointment => appointment.User)
                        .HasForeignKey(appointment => appointment.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                        .HasMany<LabResult>(appointment => appointment.LabResults)
                        .WithOne(labResult => labResult.Appointment)
                        .HasForeignKey(labTest => labTest.AppointmentId)
                        .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "property configurations"
            #region user
            modelBuilder.Entity<User>()
                        .HasIndex(user => user.Username)
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .Property(user => user.Username)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Password)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Role)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Firstname)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Lastname)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(user => user.Email)
                        .IsRequired();
            #endregion
            #region patient
            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.Firstname)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.Lastname) 
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.Phone)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.Address)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.IdentificationCard)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.Birthdate)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.IsSmoker)
                        .IsRequired();

            modelBuilder.Entity<Patient>()
                        .Property(patient => patient.HasAllergy)
                        .IsRequired();
            #endregion
            #region doctor
            modelBuilder.Entity<Doctor>()
                        .Property(doctor => doctor.Firstname) 
                        .IsRequired();

            modelBuilder.Entity<Doctor>()
                        .Property(doctor => doctor.Lastname)
                        .IsRequired();

            modelBuilder.Entity<Doctor>()
                        .Property(doctor => doctor.Email)
                        .IsRequired();

            modelBuilder.Entity<Doctor>()
                        .Property(doctor => doctor.Phone)
                        .IsRequired();

            modelBuilder.Entity<Doctor>()
                        .Property(doctor => doctor.IdentificationCard)
                        .IsRequired();
            #endregion
            #region labTest
            modelBuilder.Entity<LabTest>()
                        .Property(labTest => labTest.Name)
                        .IsRequired();
            #endregion
            #region labResult
            modelBuilder.Entity<LabResult>()
                        .Property (labResult => labResult.Status)
                        .IsRequired();
            #endregion
            #region appointment
            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.Date)
                        .IsRequired();

            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.Hour)
                        .IsRequired();

            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.Cause)
                        .IsRequired();

            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.Status)
                        .IsRequired();

            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.PatientId) 
                        .IsRequired();

            modelBuilder.Entity<Appointment>()
                        .Property(appointment => appointment.DoctorId) 
                        .IsRequired();
            #endregion
            #endregion
        }
    }
}
