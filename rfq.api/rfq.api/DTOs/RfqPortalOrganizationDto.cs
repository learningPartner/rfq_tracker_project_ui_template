using System;

namespace rfq.api.DTOs;

public class CreateRfqPortalOrganizationDto
{
    public string OrganizationType { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? CompanyCode { get; set; }
    public string? GstNumber { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
}

public class RfqPortalOrganizationDto
{
    public int OrganizationId { get; set; }
    public string OrganizationType { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string? CompanyCode { get; set; }
    public string? GstNumber { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public bool? IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class RegisterOrganizationResponseDto
{
    public RfqPortalOrganizationDto Organization { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
