using System;

namespace ProiectConta.DetailedEntries
{
    public class CreateUpdateDetailedEntryDto
    {
        public Guid EntryId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
