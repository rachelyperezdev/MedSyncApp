using MedSyncApp.Core.Application.Enums;

namespace MedSyncApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; } 
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
