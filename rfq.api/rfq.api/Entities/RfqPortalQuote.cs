using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities
{
    [Table("rfq_portal_quotes", Schema = "b31testingUser")]
    public class RfqPortalQuote
    {
        [Key]
        [Column("quote_id")]
        public int QuoteId { get; set; }

        [Required]
        [Column("rfq_id")]
        public int RfqId { get; set; }

        [Required]
        [Column("supplier_organization_id")]
        public int SupplierOrganizationId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("quote_number")]
        public string QuoteNumber { get; set; } = string.Empty;

        [Required]
        [Column("total_amount", TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(10)]
        [Column("currency")]
        public string Currency { get; set; } = "USD";

        [Column("valid_until", TypeName = "date")]
        public DateTime? ValidUntil { get; set; }

        [Column("lead_time_days")]
        public int? LeadTimeDays { get; set; }

        [MaxLength(100)]
        [Column("payment_terms")]
        public string? PaymentTerms { get; set; }

        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Submitted";

        [Column("remarks")]
        public string? Remarks { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RfqPortalQuoteItem> QuoteItems { get; set; } = new List<RfqPortalQuoteItem>();
    }
    [Table("rfq_portal_quote_items", Schema = "b31testingUser")]
    public class RfqPortalQuoteItem
    {
        [Key]
        [Column("quote_item_id")]
        public int QuoteItemId { get; set; }

        [Required]
        [Column("quote_id")]
        public int QuoteId { get; set; }

        [Required]
        [Column("rfq_item_id")]
        public int RfqItemId { get; set; }

        [Required]
        [Column("unit_price", TypeName = "decimal(18, 4)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column("total_price", TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Column("lead_time_days")]
        public int? LeadTimeDays { get; set; }

        [Column("remarks")]
        public string? Remarks { get; set; }

        [ForeignKey("QuoteId")]
        public RfqPortalQuote? Quote { get; set; }
    }
}
