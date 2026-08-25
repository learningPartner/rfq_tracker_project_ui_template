using System;
using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories;

public class RfqPortalOrganizationRepository: IRfqPortalOrganizationRepository
{
    private readonly ApplicationDbContext _context;

    public RfqPortalOrganizationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RfqPortalOrganization?> GetByCompanyCodeAsync(string companyCode)
    {
        return await _context.RfqPortalOrganizations
            .FirstOrDefaultAsync(o => o.CompanyCode == companyCode);
    }

    public async Task<RfqPortalOrganization?> GetByGstNumberAsync(string gstNumber)
    {
        return await _context.RfqPortalOrganizations
            .FirstOrDefaultAsync(o => o.GstNumber == gstNumber);
    }

    public async Task<RfqPortalOrganization> AddAsync(RfqPortalOrganization organization)
    {
        _context.RfqPortalOrganizations.Add(organization);
        await _context.SaveChangesAsync();
        return organization;
    }

}
