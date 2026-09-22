namespace rfq.api.DTOs
{
    public class RfqAttachmentDtos
    {
        public int AttachmentId { get; set; }
        public int RfqId { get; set; }
        public int? RfqItemId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public int UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
    public class CreateRfqAttachmentDto
    {
        public int RfqId { get; set; }
        public int? RfqItemId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public int UploadedByUserId { get; set; }
    }
}
