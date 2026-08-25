using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities;

[Table("rfq_portal_organizations")]
public class RfqPortalOrganization
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("organization_type")]
    public string OrganizationType { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    [Column("company_name")]
    public string CompanyName { get; set; } = null!;

    [MaxLength(50)]
    [Column("company_code")]
    public string? CompanyCode { get; set; }

    [MaxLength(30)]
    [Column("gst_number")]
    public string? GstNumber { get; set; }

    [MaxLength(100)]
    [Column("contact_person")]
    public string? ContactPerson { get; set; }

    [MaxLength(150)]
    [Column("contact_email")]
    public string? ContactEmail { get; set; }

    [MaxLength(20)]
    [Column("contact_phone")]
    public string? ContactPhone { get; set; }

    [MaxLength(300)]
    [Column("address")]
    public string? Address { get; set; }

    [MaxLength(100)]
    [Column("city")]
    public string? City { get; set; }

    [MaxLength(100)]
    [Column("state")]
    public string? State { get; set; }

    [MaxLength(100)]
    [Column("country")]
    public string? Country { get; set; }

    [MaxLength(20)]
    [Column("postal_code")]
    public string? PostalCode { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

