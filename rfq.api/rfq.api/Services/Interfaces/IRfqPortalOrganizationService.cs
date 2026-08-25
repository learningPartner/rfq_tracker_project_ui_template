using System;
using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalOrganizationService
{
    Task<ApiResponse<RegisterOrganizationResponseDto>> RegisterAsync(CreateRfqPortalOrganizationDto dto);
}
