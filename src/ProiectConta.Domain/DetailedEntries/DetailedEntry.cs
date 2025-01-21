using ProiectConta.Entries;
using ProiectConta.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.DetailedEntries
{
    public class DetailedEntry : FullAuditedEntity<Guid>
    {
        public Guid EntryId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float Value;

        private DetailedEntry()
        {

        }

        internal DetailedEntry(
            Guid id,
            Guid entryId, 
            Guid productId, 
            int quantity, 
            float value)
        {
            Id = id;
            EntryId = entryId;
            ProductId = productId;
            Quantity = quantity;
            Value = value;
        }
    }
}
