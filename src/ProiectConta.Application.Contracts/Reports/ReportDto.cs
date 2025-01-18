using System;

namespace ProiectConta.Reports
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public int GestionId { get; set; }
        public string GestionName { get; set; }
    }
}
