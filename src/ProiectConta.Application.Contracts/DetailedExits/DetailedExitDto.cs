using System;

namespace ProiectConta.DetailedExits
{
    public class DetailedExitDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }
        public Guid ExitId { get; set; }
    }
}
