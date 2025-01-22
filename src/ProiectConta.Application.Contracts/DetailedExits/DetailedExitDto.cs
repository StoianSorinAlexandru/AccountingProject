using System;

namespace ProiectConta.DetailedExits
{
    public class DetailedExitDto
    {
        public Guid Id { get; set; }
        public Guid ExitId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Value { get; set; }
    }
}
