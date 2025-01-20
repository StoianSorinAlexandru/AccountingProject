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
    public class Entry : FullAuditedAggregateRoot<Guid>
    {
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        public Guid GestionId { get; set; }

        private Entry()
        {

        }

        internal Entry(
            Guid id,
            DateTime date,
            Guid partnerId,
            Guid gestionId) : base(id)
        {
            Date = date;
            PartnerId = partnerId;
            GestionId = gestionId;
        }
    }
}
