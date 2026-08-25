using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities;

[Table("rfq_portal_users")]
public class RfqPortalUser
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey(nameof(Organization))]
    [Column("organization_id")]
    public int OrganizationId { get; set; }

    public RfqPortalOrganization? Organization { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    [Column("last_name")]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("email")]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(500)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [MaxLength(100)]
    [Column("designation")]
    public string? Designation { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("role")]
    public string Role { get; set; } = null!;

    [MaxLength(20)]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}