using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rfq.api.Entities
{
    [Table("rfq_portal_rfq_attachments", Schema = "b31testingUser")]
    public class RfqPortalRfqAttachment
    {
        [Key]
        [Column("attachment_id")]
        public int AttachmentId { get; set; }

        [Required]
        [Column("rfq_id")]
        public int RfqId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("file_name")]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [Column("file_url")]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("file_type")]
        public string FileType { get; set; } = string.Empty;

        [Column("file_size_bytes")]
        public long FileSizeBytes { get; set; }

        [Column("uploaded_by_user_id")]
        public int UploadedByUserId { get; set; }

        [Column("uploaded_at")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        [Column("rfq_item_id")]
        public int? RfqItemId { get; set; }

        [ForeignKey("RfqId")]
        public RfqPortalRfq? Rfq { get; set; }
    }
}
