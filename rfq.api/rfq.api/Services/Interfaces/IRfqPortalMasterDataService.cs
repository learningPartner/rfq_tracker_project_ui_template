using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces;

public interface IRfqPortalMasterDataService
{
    Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetByTypeAsync(string type);
}