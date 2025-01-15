namespace Data.Models
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }        
        public int OrderId { get; set; }         
        public string InvoiceNumber { get; set; } = string.Empty;  
        public decimal TotalAmount { get; set; }  
    }
}