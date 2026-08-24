using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities;

[Table("rfq_portal_master_data")]
public class RfqPortalMasterData
{
    [Key]
    [Column("master_data_id")]
    public int MasterDataId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("value")]
    public string Value { get; set; } = string.Empty;
}