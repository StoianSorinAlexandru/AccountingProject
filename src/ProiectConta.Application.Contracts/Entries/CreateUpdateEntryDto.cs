using System;

namespace ProiectConta.Entries
{
    public class CreateUpdateEntryDto
    {
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        public Guid GestionId { get; set; }
    }
}
