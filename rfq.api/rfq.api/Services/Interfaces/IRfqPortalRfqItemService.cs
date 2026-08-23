using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalRfqItemService
{
    Task<ApiResponse<RfqPortalRfqItemDto>> GetByIdAsync(int rfqItemId);

    Task<ApiResponse<IEnumerable<RfqPortalRfqItemDto>>> GetByRfqIdAsync(int rfqId);

    Task<ApiResponse<RfqPortalRfqItemDto>> CreateAsync(
        int rfqId,
        CreateRfqPortalRfqItemDto createDto);

    Task<ApiResponse<RfqPortalRfqItemDto>> UpdateAsync(
        int rfqId,
        int rfqItemId,
        UpdateRfqPortalRfqItemDto updateDto);

    Task<ApiResponse<bool>> DeleteAsync(
        int rfqId,
        int rfqItemId);
}