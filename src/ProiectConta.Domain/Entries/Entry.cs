using ProiectConta.Gestions;
using ProiectConta.Partners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.Entries
{
    public class Entry : AuditedAggregateRoot<Guid>
    {
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }
        public Guid GestionId { get; set; }
        [ForeignKey("GestionId")]
        public virtual Gestion Gestion { get; set; }
    }
}
