using MedSyncApp.Core.Domain.Common;

namespace MedSyncApp.Core.Domain.Entities
{
    public class LabTest : AuditableBaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }

        public ICollection<LabResult>? LabResults { get; set; }
        public User? User { get; set; }
    }
}
