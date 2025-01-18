using System;

namespace ProiectConta.Partners
{
    public class CreateUpdatePartnerDto
    {
        public string Name { get; set; }
        public string CUI { get; set; }
        public string Address { get; set; }
        public PartnerType Type { get; set; }
    }
}
