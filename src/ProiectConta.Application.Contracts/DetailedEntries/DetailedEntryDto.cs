using System;

namespace ProiectConta.DetailedEntries
{
    public class DetailedEntryDto
    {
        public Guid Id { get; set; }
        public Guid EntryId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Value { get; set; }
    }
}
