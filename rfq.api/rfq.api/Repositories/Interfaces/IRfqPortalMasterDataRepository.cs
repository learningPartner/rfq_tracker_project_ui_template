using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces;

public interface IRfqPortalMasterDataRepository
{
    Task<IEnumerable<RfqPortalMasterData>> GetAllAsync();
    Task<RfqPortalMasterData?> GetByIdAsync(int masterDataId);
    Task<IEnumerable<RfqPortalMasterData>> GetByTypeAsync(string type);
    Task<RfqPortalMasterData> AddAsync(RfqPortalMasterData masterData);
    Task<RfqPortalMasterData> UpdateAsync(RfqPortalMasterData masterData);
    Task DeleteAsync(int masterDataId);
    Task<bool> ExistsAsync(int masterDataId);
}
