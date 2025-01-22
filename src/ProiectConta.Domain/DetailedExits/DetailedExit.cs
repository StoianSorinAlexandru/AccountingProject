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
        public Guid ExitId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float Value;

        private DetailedExit()
        {
        }

        internal DetailedExit(
            Guid id,
            Guid exitId,
            Guid productId,
            int quantity,
            float value)
        {
            Id = id;
            ExitId = exitId;
            ProductId = productId;
            Quantity = quantity;
            Value = value;
        }
    }
}
