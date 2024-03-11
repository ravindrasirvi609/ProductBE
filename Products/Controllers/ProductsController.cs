using Microsoft.AspNetCore.Mvc;
using ProductNamespace.DTOs;
using ProductNamespace.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = productCreateDTO.Name,
                Description = productCreateDTO.Description,
                Price = productCreateDTO.Price
            };

            if (productCreateDTO.Image1 != null)
            {
                product.Image1Url = await SaveImage((Stream)productCreateDTO.Image1);
            }

            if (!string.IsNullOrEmpty(productCreateDTO.Image2Base64))
            {
                product.Image1Url = await SaveBase64Image(productCreateDTO.Image2Base64);
            }

            await _productRepository.AddProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductUpdateDTO productUpdateDTO)
        {
            if (id != productUpdateDTO.Id)
            {
                return BadRequest();
            }

            var existingProduct = await _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = productUpdateDTO.Name;
            existingProduct.Description = productUpdateDTO.Description;
            existingProduct.Price = productUpdateDTO.Price;

            if (productUpdateDTO.Image1 != null)
            {
                existingProduct.Image1Url = await SaveImage((Stream)productUpdateDTO.Image1);
            }

            if (!string.IsNullOrEmpty(productUpdateDTO.Image2Base64))
            {
                existingProduct.Image1Url = await SaveBase64Image(productUpdateDTO.Image2Base64);
            }

            await _productRepository.UpdateProduct(existingProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProduct(product);

            return NoContent();
        }

        [HttpPost("upload/image1")]
        public async Task<ActionResult<string>> UploadImage1([FromForm] ImageUploadDTO imageUploadDTO)
        {
            if (imageUploadDTO.Image == null || imageUploadDTO.Image.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            return await SaveImage((Stream)imageUploadDTO.Image);
        }

        [HttpPost("upload/image2")]
        public async Task<ActionResult<string>> UploadImage2([FromBody] string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                return BadRequest("Base64 encoded image string is required.");
            }

            return await SaveBase64Image(base64Image);
        }

        private async Task<string> SaveImage(Stream image)
        {
            try
            {
                string fileName = GenerateFileName(".jpg");
                string filePath = await SaveImageToFile(image, fileName);
                return Path.Combine("Images", fileName);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to save image.", ex);
            }
        }

        private async Task<string> SaveBase64Image(string base64Image)
        {
            try
            {
                string fileName = GenerateFileName(".png");
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                string filePath = Path.Combine("Images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                return filePath;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to save base64 image.", ex);
            }
        }

        private string GenerateFileName(string extension)
        {
            return Guid.NewGuid().ToString() + extension;
        }

        private async Task<string> SaveImageToFile(Stream image, string fileName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return filePath;
        }
    }
}
