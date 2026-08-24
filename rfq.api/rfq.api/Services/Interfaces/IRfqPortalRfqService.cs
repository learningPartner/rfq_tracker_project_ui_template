using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalRfqService
{
    Task<ApiResponse<RfqPortalRfqDto>> GetByIdAsync(int rfqId);
    // New: filtered and paginated get method. All filters optional; page/pageSize control pagination.
    Task<ApiResponse<PaginatedResult<RfqPortalRfqDto>>> GetAllAsync(string? status = null, string? industry = null, string? category = null, int page = 1, int pageSize = 10);
    Task<ApiResponse<RfqPortalRfqDto>> GetByRfqNumberAsync(string rfqNumber);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByClientOrganizationIdAsync(int clientOrganizationId);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByStatusAsync(string status);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByIndustryAsync(string industry);
    Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByCategoryAsync(string category);
    Task<ApiResponse<RfqPortalRfqDto>> CreateAsync(CreateRfqPortalRfqDto createDto);
    Task<ApiResponse<RfqPortalRfqDto>> UpdateAsync(UpdateRfqPortalRfqDto updateDto);
    Task<ApiResponse<bool>> DeleteAsync(int rfqId);
}
