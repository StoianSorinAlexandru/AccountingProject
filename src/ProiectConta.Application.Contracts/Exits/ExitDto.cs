using System;

namespace ProiectConta.Exits
{
    public class ExitDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string PartnerName { get; set; }
        public string GestionName { get; set; }
    }
}
