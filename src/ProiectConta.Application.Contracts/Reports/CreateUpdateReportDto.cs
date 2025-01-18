using System;

namespace ProiectConta.Reports
{
    public class CreateUpdateReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public int GestionId { get; set; }
        public string GestionName { get; set; }
    }
}
