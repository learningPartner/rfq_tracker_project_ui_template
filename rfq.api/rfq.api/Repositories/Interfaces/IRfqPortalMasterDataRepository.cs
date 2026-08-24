using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces;

public interface IRfqPortalMasterDataRepository
{
    Task<IEnumerable<RfqPortalMasterData>> GetByTypeAsync(string type);
}