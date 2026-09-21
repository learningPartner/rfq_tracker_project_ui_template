using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces
{
    public interface IRfqPortalRfqItemRepositoryService
    {
        Task<ApiResponse<IEnumerable<RfqItemDto>>> GetItemsByRfqIdAsync(int rfqId);
        Task<ApiResponse<RfqItemDto>> GetItemByIdAsync(int id);
        Task<ApiResponse<RfqItemDto>> CreateItemAsync(CreateRfqItemDto dto);
        Task<ApiResponse<RfqItemDto>> UpdateItemAsync(int id, UpdateRfqItemDto dto);
        Task<ApiResponse<bool>> DeleteItemAsync(int id);
    }
}
