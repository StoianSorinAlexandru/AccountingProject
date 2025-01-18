using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.Partners
{
    public class Partner : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string CUI { get; set; }
        public string Address { get; set; }
        public PartnerType Type { get; set; }

    }
}
