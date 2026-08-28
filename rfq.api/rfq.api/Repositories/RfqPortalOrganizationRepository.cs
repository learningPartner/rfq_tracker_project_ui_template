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

    public async Task<IEnumerable<RfqPortalOrganization?>> GetAllAsync()
    {
        return await _context.RfqPortalOrganizations.AsNoTracking().ToListAsync();
    }
    public async Task<RfqPortalOrganization?> GetByIdAsync(int organizationId)
    {
        return await _context.RfqPortalOrganizations.FindAsync(organizationId);
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

    public async Task<RfqPortalOrganization> UpdateAsync(RfqPortalOrganization organization)
    {
        _context.RfqPortalOrganizations.Update(organization);
        await _context.SaveChangesAsync();
        return organization;
    }

    public async Task DeleteAsync(int organizationId)
    {
        var organization = await _context.RfqPortalOrganizations.FindAsync(organizationId);
        if (organization != null)
        {
            organization.IsActive = false;
            _context.RfqPortalOrganizations.Update(organization);
            await _context.SaveChangesAsync();
        }
    }

}
