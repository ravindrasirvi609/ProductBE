using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(0, double.MaxValue)]  // Adjust data type and range as needed
    public decimal Price { get; set; }
    public string Image1Url { get; set; }
    public string Image2Base64 { get; set; }
}
