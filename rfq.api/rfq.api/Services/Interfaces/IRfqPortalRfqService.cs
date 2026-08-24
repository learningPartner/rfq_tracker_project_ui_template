using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalRfqService
{
    Task<ApiResponse<RfqPortalRfqDto>> GetByIdAsync(int rfqId);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetAllAsync(
      string? search,
      string? status,
      string? industry,
      string? category);
    Task<ApiResponse<RfqPortalRfqDto>> GetByRfqNumberAsync(string rfqNumber);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByClientOrganizationIdAsync(int clientOrganizationId);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByStatusAsync(string status);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByIndustryAsync(string industry);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByCategoryAsync(string category);
    Task<ApiResponse<RfqPortalRfqDto>> CreateAsync(CreateRfqPortalRfqDto createDto);
    Task<ApiResponse<RfqPortalRfqDto>> UpdateAsync(UpdateRfqPortalRfqDto updateDto);
    Task<ApiResponse<bool>> DeleteAsync(int rfqId);
}
