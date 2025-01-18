using System;

namespace ProiectConta.Exits
{
    public class ExitDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        public Guid GestionId { get; set; }
    }
}
