using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces;

public interface IRfqPortalOrganizationRepository
{
    Task<IEnumerable<RfqPortalOrganization?>> GetAllAsync();
    Task<RfqPortalOrganization?> GetByIdAsync(int organizationId);
    Task<RfqPortalOrganization?> GetByCompanyCodeAsync(string companyCode);
    Task<RfqPortalOrganization?> GetByGstNumberAsync(string gstNumber);
    Task<RfqPortalOrganization> AddAsync(RfqPortalOrganization organization);
    Task<RfqPortalOrganization> UpdateAsync(RfqPortalOrganization organization);
    Task DeleteAsync(int organizationId);

}
