using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces;

public interface IRfqPortalOrganizationRepository
{
    Task<RfqPortalOrganization?> GetByCompanyCodeAsync(string companyCode);
    Task<RfqPortalOrganization?> GetByGstNumberAsync(string gstNumber);
    Task<RfqPortalOrganization> AddAsync(RfqPortalOrganization organization);

}
