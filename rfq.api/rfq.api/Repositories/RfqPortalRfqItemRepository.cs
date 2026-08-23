using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories;

public class RfqPortalRfqItemRepository : IRfqPortalRfqItemRepository
{
    private readonly ApplicationDbContext _context;

    public RfqPortalRfqItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RfqPortalRfqItem?> GetByIdAsync(int rfqItemId)
    {
        return await _context.RfqPortalRfqItems.FindAsync(rfqItemId);
    }

    public async Task<IEnumerable<RfqPortalRfqItem>> GetByRfqIdAsync(int rfqId)
    {
        return await _context.RfqPortalRfqItems
            .Where(r => r.RfqId == rfqId)
            .OrderBy(r => r.LineNumber)
            .ToListAsync();
    }

    public async Task<RfqPortalRfqItem> AddAsync(RfqPortalRfqItem item)
    {
        await _context.RfqPortalRfqItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(RfqPortalRfqItem item)
    {
        _context.RfqPortalRfqItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RfqPortalRfqItem item)
    {
        _context.RfqPortalRfqItems.Remove(item);
        await _context.SaveChangesAsync();
    }
}