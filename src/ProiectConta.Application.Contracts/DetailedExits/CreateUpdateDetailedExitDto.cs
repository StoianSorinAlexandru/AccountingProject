using System;

namespace ProiectConta.DetailedExits
{
    public class CreateUpdateDetailedExitDto
    {
        public Guid ExitId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Value { get; set; }
    }
}
