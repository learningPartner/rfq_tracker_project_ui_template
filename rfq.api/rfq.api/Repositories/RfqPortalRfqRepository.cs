using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories;

public class RfqPortalRfqRepository : IRfqPortalRfqRepository
{
    private readonly ApplicationDbContext _context;

    public RfqPortalRfqRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RfqPortalRfq?> GetByIdAsync(int rfqId)
    {
        return await _context.RfqPortalRfqs.FindAsync(rfqId);
    }

    public async Task<IEnumerable<RfqPortalRfq>> GetAllAsync()
    {
        return await _context.RfqPortalRfqs.ToListAsync();
    }

    public async Task<RfqPortalRfq?> GetByRfqNumberAsync(string rfqNumber)
    {
        return await _context.RfqPortalRfqs
            .FirstOrDefaultAsync(r => r.RfqNumber == rfqNumber);
    }

    public async Task<IEnumerable<RfqPortalRfq>> GetByClientOrganizationIdAsync(int clientOrganizationId)
    {
        return await _context.RfqPortalRfqs
            .Where(r => r.ClientOrganizationId == clientOrganizationId)
            .ToListAsync();
    }

    public async Task<IEnumerable<RfqPortalRfq>> GetByStatusAsync(string status)
    {
        return await _context.RfqPortalRfqs
            .Where(r => r.RfqStatus == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<RfqPortalRfq>> GetByIndustryAsync(string industry)
    {
        return await _context.RfqPortalRfqs
            .Where(r => r.Industry == industry)
            .ToListAsync();
    }

    public async Task<IEnumerable<RfqPortalRfq>> GetByCategoryAsync(string category)
    {
        return await _context.RfqPortalRfqs
            .Where(r => r.Category == category)
            .ToListAsync();
    }

    public async Task<RfqPortalRfq> AddAsync(RfqPortalRfq rfq)
    {
        await _context.RfqPortalRfqs.AddAsync(rfq);
        await _context.SaveChangesAsync();
        return rfq;
    }

    public async Task UpdateAsync(RfqPortalRfq rfq)
    {
        rfq.UpdatedAt = DateTime.UtcNow;
        _context.RfqPortalRfqs.Update(rfq);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RfqPortalRfq rfq)
    {
        _context.RfqPortalRfqs.Remove(rfq);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RfqNumberExistsAsync(string rfqNumber, int? excludeRfqId = null)
    {
        if (excludeRfqId.HasValue)
        {
            return await _context.RfqPortalRfqs
                .AnyAsync(r => r.RfqNumber == rfqNumber && r.RfqId != excludeRfqId.Value);
        }
        return await _context.RfqPortalRfqs.AnyAsync(r => r.RfqNumber == rfqNumber);
    }
}
