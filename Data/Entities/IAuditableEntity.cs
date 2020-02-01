using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alexander_neumann.api.Data.Entities
{
    interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
