namespace Data.Models
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; } 

        public int OrderId { get; set; }     

        public string AWBNumber { get; set; } = string.Empty;  

        public string Status { get; set; } = "Expediata";      
    }
}