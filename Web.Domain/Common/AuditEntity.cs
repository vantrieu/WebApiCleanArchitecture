using System;

namespace Web.Domain.Common
{
    public class AuditEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}
