using System.ComponentModel.DataAnnotations;

namespace rfq.api.DTOs
{
    public class QuoteDto
    {
        public int QuoteId { get; set; }
        public int RfqId { get; set; }
        public int SupplierOrganizationId { get; set; }
        public string QuoteNumber { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "USD";
        public DateTime? ValidUntil { get; set; }
        public int? LeadTimeDays { get; set; }
        public string? PaymentTerms { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<QuoteItemDto> QuoteItems { get; set; } = new();
    }
    public class QuoteItemDto
    {
        public int QuoteItemId { get; set; }
        public int QuoteId { get; set; }
        public int RfqItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? LeadTimeDays { get; set; }
        public string? Remarks { get; set; }
    }
    public class CreateQuoteDto
    {
        [Required]
        public int RfqId { get; set; }

        [Required]
        public int SupplierOrganizationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string QuoteNumber { get; set; } = string.Empty;

        [Required]
        public decimal TotalAmount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; } = "USD";

        public DateTime? ValidUntil { get; set; }

        public int? LeadTimeDays { get; set; }

        [MaxLength(100)]
        public string? PaymentTerms { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Submitted";

        public string? Remarks { get; set; }

        public List<CreateQuoteItemDto> QuoteItems { get; set; } = new();
    }

    public class CreateQuoteItemDto
    {
        [Required]
        public int RfqItemId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int? LeadTimeDays { get; set; }

        public string? Remarks { get; set; }
    }

    public class UpdateQuoteStatusDto
    {
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
