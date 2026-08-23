using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities;

[Table("rfq_portal_rfq_items")]
public class RfqPortalRfqItem
{
    [Key]
    [Column("rfq_item_id")]
    public int RfqItemId { get; set; }
    [Required]
    [Column("rfq_id")]
    public int RfqId { get; set; }
    [Required]
    [Column("line_number")]
    public int LineNumber { get; set; }

    [MaxLength(100)]
    [Column("product_code")]
    public string? ProductCode { get; set; }
    [Required]
    [MaxLength(200)]
    [Column("product_name")]
    public string ProductName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("material")]
    public string? Material { get; set; }

    [Required]
    [Column("quantity", TypeName = "decimal(18, 2)")]
    public decimal Quantity { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("unit")]
    public string Unit { get; set; } = string.Empty;

    [Column("required_date")]
    public DateTime? RequiredDate { get; set; }

    [Column("specifications")]
    public string? Specifications { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }


}