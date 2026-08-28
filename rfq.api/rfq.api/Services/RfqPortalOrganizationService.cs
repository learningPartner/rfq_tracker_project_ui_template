using System;
using Microsoft.EntityFrameworkCore;
using rfq.api.Constants;
using rfq.api.Data;
using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services;

public class RfqPortalOrganizationService : IRfqPortalOrganizationService
{
    private const string DefaultAdminPassword = "Admin@123";
    private const string DefaultAdminRole = "Admin";
    private readonly IRfqPortalOrganizationRepository _organizationRepository;
    private readonly ApplicationDbContext _context;
    public RfqPortalOrganizationService(IRfqPortalOrganizationRepository organizationRepository, ApplicationDbContext context)
    {
        _organizationRepository = organizationRepository;
        _context = context;
    }

    public async Task<ApiResponse<RfqPortalOrganizationDto>> GetByIdAsync(int organizationId)
    {
        var organization = await _organizationRepository.GetByIdAsync(organizationId);
        if (organization == null)
        {
            return ApiResponse<RfqPortalOrganizationDto>.FailureResponse(MessageConstants.OrganizationNotFound);
        }

        var organizationDto = MapToDto(organization);
        return ApiResponse<RfqPortalOrganizationDto>.SuccessResponse(organizationDto, MessageConstants.OrganizationRetrievedSuccessfully);
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalOrganizationDto>>> GetAllAsync()
    {
        var organizations = await _organizationRepository.GetAllAsync();
        var result = organizations.Select(MapToDto);
        return ApiResponse<IEnumerable<RfqPortalOrganizationDto>>.SuccessResponse(
            result, "Organizations retrieved successfully.");
    }
    public async Task<ApiResponse<RegisterOrganizationResponseDto>> RegisterAsync(CreateRfqPortalOrganizationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.ContactEmail))
        {
            return ApiResponse<RegisterOrganizationResponseDto>.FailureResponse(
                "Organization registration failed.",
                new List<string> { "ContactEmail is required to provision the organization's admin user." });
        }

        if (!string.IsNullOrWhiteSpace(dto.CompanyCode))
        {
            var existingByCode = await _organizationRepository.GetByCompanyCodeAsync(dto.CompanyCode);
            if (existingByCode is not null)
            {
                return ApiResponse<RegisterOrganizationResponseDto>.FailureResponse(
                    "Organization registration failed.",
                    new List<string> { $"An organization with company code '{dto.CompanyCode}' already exists." });
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.GstNumber))
        {
            var existingByGst = await _organizationRepository.GetByGstNumberAsync(dto.GstNumber);
            if (existingByGst is not null)
            {
                return ApiResponse<RegisterOrganizationResponseDto>.FailureResponse(
                    "Organization registration failed.",
                    new List<string> { $"An organization with GST number '{dto.GstNumber}' already exists." });
            }
        }

        var existingUser = await _context.RfqPortalUsers
           .AsNoTracking()
           .FirstOrDefaultAsync(u => u.Email == dto.ContactEmail);

        if (existingUser is not null)
        {
            return ApiResponse<RegisterOrganizationResponseDto>.FailureResponse(
                "Organization registration failed.",
                new List<string> { $"A user with email '{dto.ContactEmail}' already exists." });
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();

        var organization = new RfqPortalOrganization
        {
            OrganizationType = dto.OrganizationType,
            CompanyName = dto.CompanyName,
            CompanyCode = dto.CompanyCode,
            GstNumber = dto.GstNumber,
            ContactPerson = dto.ContactPerson,
            ContactEmail = dto.ContactEmail,
            ContactPhone = dto.ContactPhone,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            PostalCode = dto.PostalCode,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var createdOrganization = await _organizationRepository.AddAsync(organization);

        var user = new RfqPortalUser
        {
            OrganizationId = createdOrganization.OrganizationId,
            FirstName = dto.ContactPerson ?? dto.CompanyName,
            LastName = null,
            Email = dto.ContactEmail,
            PasswordHash = DefaultAdminPassword,
            Designation = DefaultAdminRole,
            Role = DefaultAdminRole,
            PhoneNumber = dto.ContactPhone,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.RfqPortalUsers.Add(user);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        var response = new RegisterOrganizationResponseDto
        {
            Organization = MapToDto(createdOrganization),
            Username = user.Email,
            Password = DefaultAdminPassword
        };

        return ApiResponse<RegisterOrganizationResponseDto>.SuccessResponse(response, MessageConstants.OrganizationCreatedSuccessfully);
    }

    public async Task<ApiResponse<RfqPortalOrganizationDto>> UpdateAsync(UpdateRfqPortalOrganizationDto dto)
    {
        var organization = await _organizationRepository.GetByIdAsync(dto.OrganizationId);
        if (organization == null)
        {
            return ApiResponse<RfqPortalOrganizationDto>.FailureResponse(MessageConstants.OrganizationNotFound);
        }

        // Check for unique CompanyCode
        if (!string.IsNullOrWhiteSpace(dto.CompanyCode) && dto.CompanyCode != organization.CompanyCode)
        {
            var existingByCode = await _organizationRepository.GetByCompanyCodeAsync(dto.CompanyCode);
            if (existingByCode is not null && existingByCode.OrganizationId != dto.OrganizationId)
            {
                return ApiResponse<RfqPortalOrganizationDto>.FailureResponse(
                    MessageConstants.OrganizationUpdateFailed,
                    new List<string> { $"An organization with company code '{dto.CompanyCode}' already exists." });
            }
        }

        // Check for unique GstNumber
        if (!string.IsNullOrWhiteSpace(dto.GstNumber) && dto.GstNumber != organization.GstNumber)
        {
            var existingByGst = await _organizationRepository.GetByGstNumberAsync(dto.GstNumber);
            if (existingByGst is not null && existingByGst.OrganizationId != dto.OrganizationId)
            {
                return ApiResponse<RfqPortalOrganizationDto>.FailureResponse(
                    MessageConstants.OrganizationUpdateFailed,
                    new List<string> { $"An organization with GST number '{dto.GstNumber}' already exists." });
            }
        }

        // Update fields
        organization.OrganizationType = dto.OrganizationType;
        organization.CompanyName = dto.CompanyName;
        organization.CompanyCode = dto.CompanyCode;
        organization.GstNumber = dto.GstNumber;
        organization.ContactPerson = dto.ContactPerson;
        organization.ContactEmail = dto.ContactEmail;
        organization.ContactPhone = dto.ContactPhone;
        organization.Address = dto.Address;
        organization.City = dto.City;
        organization.State = dto.State;
        organization.Country = dto.Country;
        organization.PostalCode = dto.PostalCode;
        organization.UpdatedAt = DateTime.UtcNow;

        await _organizationRepository.UpdateAsync(organization);

        var updatedDto = MapToDto(organization);
        return ApiResponse<RfqPortalOrganizationDto>.SuccessResponse(updatedDto, MessageConstants.OrganizationUpdatedSuccessfully);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int organizationId)
    {
        var organization = await _organizationRepository.GetByIdAsync(organizationId);
        if (organization == null)
        {
            return ApiResponse<bool>.FailureResponse(MessageConstants.OrganizationNotFound);
        }

        organization.IsActive = false;
        await _organizationRepository.UpdateAsync(organization);

        return ApiResponse<bool>.SuccessResponse(true, MessageConstants.OrganizationDeletedSuccessfully);
    }

    private static RfqPortalOrganizationDto MapToDto(RfqPortalOrganization organization)
    {
        return new RfqPortalOrganizationDto
        {
            OrganizationId = organization.OrganizationId,
            OrganizationType = organization.OrganizationType,
            CompanyName = organization.CompanyName,
            CompanyCode = organization.CompanyCode,
            GstNumber = organization.GstNumber,
            ContactPerson = organization.ContactPerson,
            ContactEmail = organization.ContactEmail,
            ContactPhone = organization.ContactPhone,
            Address = organization.Address,
            City = organization.City,
            State = organization.State,
            Country = organization.Country,
            PostalCode = organization.PostalCode,
            IsActive = organization.IsActive,
            CreatedAt = organization.CreatedAt,
            UpdatedAt = organization.UpdatedAt
        };
    }

}
