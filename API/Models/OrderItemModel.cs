using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class OrderItemModel
    {
        [Required]
        [JsonPropertyName("productName")]  
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonIgnore]  
        public decimal Price { get; set; }// clientul nu pune pretul cand plaseaza comanda
    }

}