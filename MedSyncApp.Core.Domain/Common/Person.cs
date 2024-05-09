namespace MedSyncApp.Core.Domain.Common
{
    public class Person : AuditableBaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string IdentificationCard { get; set; }
        public string? ImageURL { get; set; }
    }
}
