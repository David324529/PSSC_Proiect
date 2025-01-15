using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class ProcessOrderCommand
    {
        [Required]
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("deliveryAddress")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("orderItems")]
        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
    }

}