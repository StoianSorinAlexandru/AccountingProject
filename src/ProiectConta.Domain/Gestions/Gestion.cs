using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Emailing;

namespace ProiectConta.Gestions
{
    public class Gestion : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    
        private Gestion()
        {

        }

        internal Gestion(
            Guid id,
            string name)
        {
            SetName(name);
        }

        internal Gestion ChangeName(string name)
        {
            SetName(name);
            return this;
        }

        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
                );
        }
    }
}
