using MedSyncApp.Core.Application.ViewModels.User;

namespace MedSyncApp.Core.Application.ViewModels.LabTest
{
    public class LabTestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
