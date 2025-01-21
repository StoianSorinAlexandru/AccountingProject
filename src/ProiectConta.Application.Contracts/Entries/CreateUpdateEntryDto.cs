using System;

namespace ProiectConta.Entries
{
    public class CreateUpdateEntryDto
    {
        public DateTime Date { get; set; }
        public string PartnerName { get; set; }
        public string GestionName { get; set; }
    }
}
