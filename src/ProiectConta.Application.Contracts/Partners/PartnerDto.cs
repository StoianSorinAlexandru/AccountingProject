using System;

namespace ProiectConta.Partners
{
    public class PartnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CUI { get; set; }
        public string Address { get; set; }
        public PartnerType Type { get; set; }
    }
}
