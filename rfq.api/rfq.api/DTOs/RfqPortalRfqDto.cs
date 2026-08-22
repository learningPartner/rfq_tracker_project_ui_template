namespace rfq.api.DTOs;

public class CreateRfqPortalRfqDto
{
    public string RfqNumber { get; set; } = string.Empty;
    public int ClientOrganizationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Industry { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? ManufacturingProcess { get; set; }
    public string? Material { get; set; }
    public string? LocationCity { get; set; }
    public string? LocationState { get; set; }
    public DateTime ResponseDeadline { get; set; }
    public string? RfqStatus { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int CreatedByUserId { get; set; }
    public int? AwardedQuoteId { get; set; }
}

public class UpdateRfqPortalRfqDto
{
    public int RfqId { get; set; }
    public string RfqNumber { get; set; } = string.Empty;
    public int ClientOrganizationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Industry { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? ManufacturingProcess { get; set; }
    public string? Material { get; set; }
    public string? LocationCity { get; set; }
    public string? LocationState { get; set; }
    public DateTime ResponseDeadline { get; set; }
    public string RfqStatus { get; set; } = string.Empty;
    public DateTime? PublishedDate { get; set; }
    public int CreatedByUserId { get; set; }
    public int? AwardedQuoteId { get; set; }
}

public class RfqPortalRfqDto
{
    public int RfqId { get; set; }
    public string RfqNumber { get; set; } = string.Empty;
    public int ClientOrganizationId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Industry { get; set; }
    public string? Category { get; set; }
    public string? ManufacturingProcess { get; set; }
    public string? Material { get; set; }
    public string? LocationCity { get; set; }
    public string? LocationState { get; set; }
    public DateTime? ResponseDeadline { get; set; }
    public string? RfqStatus { get; set; }
    public DateTime? PublishedDate { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? AwardedQuoteId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
