using Microsoft.AspNetCore.Http;

namespace ProductNamespace.DTOs
{
    public class ImageUploadDTO
    {
        public IFormFile Image { get; set; }
    }
}
