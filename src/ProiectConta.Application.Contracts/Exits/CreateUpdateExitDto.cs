using System;

namespace ProiectConta.Exits
{
    public class CreateUpdateExitDto
    {
        public DateTime Date { get; set; }
        public Guid PartnerId { get; set; }
        public Guid GestionId { get; set; }
    }
}
