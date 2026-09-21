using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces
{
    public interface IRfqPortalQuoteRepository
    {
        Task<IEnumerable<RfqPortalQuote>> GetByRfqIdAsync(int rfqId);
        Task<IEnumerable<RfqPortalQuote>> GetBySupplierIdAsync(int supplierOrgId);
        Task<RfqPortalQuote?> GetByIdAsync(int id);
        Task<RfqPortalQuote> AddAsync(RfqPortalQuote entity);
        Task UpdateAsync(RfqPortalQuote entity);
        Task DeleteAsync(RfqPortalQuote entity);
        Task<bool> ExistsAsync(int id);
    }
}
