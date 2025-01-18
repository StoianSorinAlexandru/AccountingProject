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
        [ForeignKey("EntryId")]
        public virtual Entry Entry { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public float Value => Quantity * (Product?.Price ?? 0);
    }
}
