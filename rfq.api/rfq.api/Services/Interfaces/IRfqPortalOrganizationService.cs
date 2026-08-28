using System;
using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalOrganizationService
{
    Task<ApiResponse<IEnumerable<RfqPortalOrganizationDto>>> GetAllAsync();
    Task<ApiResponse<RfqPortalOrganizationDto>> GetByIdAsync(int organizationId);
    Task<ApiResponse<RegisterOrganizationResponseDto>> RegisterAsync(CreateRfqPortalOrganizationDto dto);
    Task<ApiResponse<RfqPortalOrganizationDto>> UpdateAsync(UpdateRfqPortalOrganizationDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int organizationId);
}
