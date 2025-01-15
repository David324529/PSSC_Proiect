using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class OrderModel
    {
        [Key]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }  

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        [JsonPropertyName("deliveryAddress")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("orderItems")]
        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();

        public List<string> ValidationErrors { get; set; } = new List<string>();

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "In procesare";

        public DateTime? PublishedDate { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;
    }
}