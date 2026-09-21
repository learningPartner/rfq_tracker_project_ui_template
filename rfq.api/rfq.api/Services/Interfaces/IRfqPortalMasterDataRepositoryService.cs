using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalMasterDataService
{
    Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetAllAsync();
    Task<ApiResponse<RfqPortalMasterDataDto>> GetByIdAsync(int masterDataId);
    Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetByTypeAsync(string type);
    Task<ApiResponse<RfqPortalMasterDataDto>> CreateAsync(CreateRfqPortalMasterDataDto dto);
    Task<ApiResponse<RfqPortalMasterDataDto>> UpdateAsync(UpdateRfqPortalMasterDataDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int masterDataId);
    Task<ApiResponse<List<string>>> GetAllTypesAsync();
}
