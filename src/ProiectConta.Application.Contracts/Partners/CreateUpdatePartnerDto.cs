using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectConta.Partners
{
    public class CreateUpdatePartnerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CUI { get; set; }
        [Required]
        public string Address { get; set; }
        public PartnerType Type { get; set; }
    }
}
