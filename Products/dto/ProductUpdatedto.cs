using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ProductNamespace.DTOs
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile Image1 { get; set; }

        // Assuming Image2 will be provided as base64 string
        public string Image2Base64 { get; set; }
    }
}
