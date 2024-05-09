using MedSyncApp.Core.Application.Enums;
using MedSyncApp.Core.Application.ViewModels.User;

namespace MedSyncApp.Core.Application.ViewModels.LabResult
{
    public class LabResultViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientFullname { get; set; }
        public string PatientIdentificationCard { get; set; }
        public string? LabTestName { get; set; }
        public LabResultStatus? Result { get; set; }
        public string? ResultText { get; set; }
        public string Status { get; set; }
        public int? LabTestId { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }

        public int AppointmentId { get; set; }
    }
}
