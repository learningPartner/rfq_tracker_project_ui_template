using System.ComponentModel.DataAnnotations;

namespace rfq.api.DTOs
{
    public class RfqItemDto
    {
        public int RfqItemId { get; set; }
        public int RfqId { get; set; }
        public int LineNumber { get; set; }
        public string? ProductCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Material { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime? RequiredDate { get; set; }
        public string? Specifications { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public class CreateRfqItemDto
    {
        [Required]
        public int RfqId { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [MaxLength(100)]
        public string? ProductCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Material { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Unit { get; set; } = string.Empty;

        public DateTime? RequiredDate { get; set; }

        public string? Specifications { get; set; }
    }

    public class UpdateRfqItemDto
    {
        [Required]
        public int LineNumber { get; set; }

        [MaxLength(100)]
        public string? ProductCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Material { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Unit { get; set; } = string.Empty;

        public DateTime? RequiredDate { get; set; }

        public string? Specifications { get; set; }
    }
}
