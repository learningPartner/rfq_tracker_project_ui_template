namespace rfq.api.DTOs;

public class CreateRfqPortalRfqItemDto

{
    public int LineNumber { get; set; }
    public string? ProductCode { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Material { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime? RequiredDate { get; set; }
    public string? Specifications { get; set; }
}

public class UpdateRfqPortalRfqItemDto
{
    public int LineNumber { get; set; }
    public string? ProductCode { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Material { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime? RequiredDate { get; set; }
    public string? Specifications { get; set; }
}

public class RfqPortalRfqItemDto
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

