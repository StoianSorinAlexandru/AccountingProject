using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.Partners
{
    public class Partner : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string CUI { get; set; }
        public string Address { get; set; }
        public PartnerType Type { get; set; }

        private Partner()
        {
        }

        internal Partner(
            Guid id,
            string name,
            string cui,
            string address,
            PartnerType type) : base(id)
        {
            SetName(name);
            SetCUI(cui);
            SetAddress(address);
            Type = type;
        }

        internal Partner ChangeName(string name)
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

        private void SetCUI(string cui)
        {
            CUI = Check.NotNullOrWhiteSpace(
                cui,
                nameof(cui)
            );
        }

        private void SetAddress(string address)
        {
            Address = Check.NotNullOrWhiteSpace(
                address,
                nameof(address)
            );
        }

    }
}
