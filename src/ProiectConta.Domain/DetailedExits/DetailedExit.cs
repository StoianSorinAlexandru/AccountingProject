using ProiectConta.Exits;
using ProiectConta.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProiectConta.DetailedExits
{
    public class DetailedExit : FullAuditedEntity<Guid>
    {
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }
        public Guid ExitId { get; set; }
        [ForeignKey("ExitId")]
        public virtual Exit Exit { get; set; }
    }
}
