using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces
{
    public interface IRfqPortalRfqItemRepository
    {
        Task <RfqPortalRfqItem?> GetByIdAsync(int rfqItemId);
        Task<IEnumerable<RfqPortalRfqItem>> GetByRfqIdAsync(int rfqId);
         Task<RfqPortalRfqItem?> AddAsync(RfqPortalRfqItem item);
        Task UpdateAsync(RfqPortalRfqItem item);
        Task DeleteAsync(RfqPortalRfqItem item);
    }
}
