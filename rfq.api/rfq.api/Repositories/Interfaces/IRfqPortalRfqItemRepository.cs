using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces
{
    public interface IRfqPortalRfqItemRepository
    {
        Task<IEnumerable<RfqPortalRfqItem>> GetByRfqIdAsync(int rfqId);
        Task<RfqPortalRfqItem?> GetByIdAsync(int id);
        Task<RfqPortalRfqItem> AddAsync(RfqPortalRfqItem entity);
        Task UpdateAsync(RfqPortalRfqItem entity);
        Task DeleteAsync(RfqPortalRfqItem entity);
        Task<bool> ExistsAsync(int id);
    }
}
