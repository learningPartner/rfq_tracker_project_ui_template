using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories
{
    public class RfqPortalRfqItemRepository : IRfqPortalRfqItemRepository
    {
        private readonly ApplicationDbContext _context;
        public RfqPortalRfqItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RfqPortalRfqItem>> GetByRfqIdAsync(int rfqId)
        {
            return await _context.Set<RfqPortalRfqItem>()
                .AsNoTracking()
                .Where(x => x.RfqId == rfqId)
                .OrderBy(x => x.LineNumber)
                .ToListAsync();
        }

        public async Task<RfqPortalRfqItem?> GetByIdAsync(int id)
        {
            return await _context.Set<RfqPortalRfqItem>()
                .FindAsync(id);
        }

        public async Task<RfqPortalRfqItem> AddAsync(RfqPortalRfqItem entity)
        {
            await _context.Set<RfqPortalRfqItem>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(RfqPortalRfqItem entity)
        {
            _context.Set<RfqPortalRfqItem>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RfqPortalRfqItem entity)
        {
            _context.Set<RfqPortalRfqItem>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<RfqPortalRfqItem>()
                .AnyAsync(x => x.RfqItemId == id);
        }
    }
}
