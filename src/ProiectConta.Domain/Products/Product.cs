using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public float? Price { get; set; }

        private Product()
        {

        }

        internal Product(
            Guid id,
            string name,
            float price) : base(id)
        {
            SetName(name);
            Price = price;
        }

        internal Product ChangeName(string name)
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
