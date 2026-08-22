using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities;

[Table("rfq_portal_rfqs")]
public class RfqPortalRfq
{
    [Key]
    [Column("rfq_id")]
    public int RfqId { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("rfq_number")]
    public string RfqNumber { get; set; } = string.Empty;

    [Required]
    [Column("client_organization_id")]
    public int ClientOrganizationId { get; set; }

    [Required]
    [MaxLength(250)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [MaxLength(100)]
    [Column("industry")]
    public string? Industry { get; set; }

    [MaxLength(100)]
    [Column("category")]
    public string? Category { get; set; }

    [MaxLength(100)]
    [Column("manufacturing_process")]
    public string? ManufacturingProcess { get; set; }

    [MaxLength(100)]
    [Column("material")]
    public string? Material { get; set; }

    [MaxLength(100)]
    [Column("location_city")]
    public string? LocationCity { get; set; }

    [MaxLength(100)]
    [Column("location_state")]
    public string? LocationState { get; set; }

    [Column("response_deadline")]
    public DateTime? ResponseDeadline { get; set; }

    [MaxLength(20)]
    [Column("rfq_status")]
    public string? RfqStatus { get; set; }

    [Column("published_date")]
    public DateTime? PublishedDate { get; set; }

    [Column("created_by_user_id")]
    public int? CreatedByUserId { get; set; }

    [Column("awarded_quote_id")]
    public int? AwardedQuoteId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
