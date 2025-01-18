using System;

namespace ProiectConta.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float? Price { get; set; }
    }
}
