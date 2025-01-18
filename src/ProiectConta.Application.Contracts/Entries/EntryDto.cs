using System;

namespace ProiectConta.Entries
{
    public class EntryDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        public Guid GestionId { get; set; }
    }
}
