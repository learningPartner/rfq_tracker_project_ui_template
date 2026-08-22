using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces;

public interface IRfqPortalRfqRepository
{
    Task<RfqPortalRfq?> GetByIdAsync(int rfqId);
    Task<IEnumerable<RfqPortalRfq>> GetAllAsync();
    Task<RfqPortalRfq?> GetByRfqNumberAsync(string rfqNumber);
    Task<IEnumerable<RfqPortalRfq>> GetByClientOrganizationIdAsync(int clientOrganizationId);
    Task<IEnumerable<RfqPortalRfq>> GetByStatusAsync(string status);
    Task<IEnumerable<RfqPortalRfq>> GetByIndustryAsync(string industry);
    Task<IEnumerable<RfqPortalRfq>> GetByCategoryAsync(string category);
    Task<RfqPortalRfq> AddAsync(RfqPortalRfq rfq);
    Task UpdateAsync(RfqPortalRfq rfq);
    Task DeleteAsync(RfqPortalRfq rfq);
    Task<bool> RfqNumberExistsAsync(string rfqNumber, int? excludeRfqId = null);
}
