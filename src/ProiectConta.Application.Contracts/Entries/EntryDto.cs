using System;

namespace ProiectConta.Entries
{
    public class EntryDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string PartnerName { get; set; }
        public string GestionName { get; set; }
    }
}
