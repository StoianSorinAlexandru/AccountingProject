using System.ComponentModel.DataAnnotations;

namespace ProiectConta.Products
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string Name { get; set; }
        public float? Price { get; set; }
    }
}
