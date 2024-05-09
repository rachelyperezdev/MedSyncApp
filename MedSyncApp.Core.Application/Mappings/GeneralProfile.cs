using AutoMapper;
using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.Appointment;
using MedSyncApp.Core.Application.ViewModels.Doctor;
using MedSyncApp.Core.Application.ViewModels.LabResult;
using MedSyncApp.Core.Application.ViewModels.LabTest;
using MedSyncApp.Core.Application.ViewModels.Patient;
using MedSyncApp.Core.Application.ViewModels.User;
using MedSyncApp.Core.Domain.Entities;

namespace MedSyncApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<User, SaveUserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Role)))
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<User, UpdateUserViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<UpdateUserViewModel, SaveUserViewModel>();

            #endregion

            #region DoctorProfile
            CreateMap<Doctor, DoctorViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<Doctor, SaveDoctorViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());
            #endregion

            #region LabTestProfile
            CreateMap<LabTest, LabTestViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<LabTest, SaveLabTestViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());
            #endregion

            #region PatientProfile
            CreateMap<Patient, PatientViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<Patient, SavePatientViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());
            #endregion

            #region LabResultProfile
            CreateMap<LabResult, LabResultViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(Role), src.Status)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<LabResult, SaveLabResultViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());
            #endregion

            #region AppointmentProfile
            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.Firstname} {src.Patient.Lastname}"))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.Firstname} {src.Doctor.Lastname}"))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<AppointmentStatus>(src.Status)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Patient, opt => opt.Ignore())
                .ForMember(dest => dest.Doctor, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());

            CreateMap<Appointment, SaveAppointmentViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<AppointmentStatus>(src.Status)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifyBy, opt => opt.Ignore());
            #endregion
        }
    }
}
