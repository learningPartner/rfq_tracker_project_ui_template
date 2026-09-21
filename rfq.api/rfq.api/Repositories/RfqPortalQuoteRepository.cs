using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories
{
    public class RfqPortalQuoteRepository: IRfqPortalQuoteRepository
    {

        private readonly ApplicationDbContext _context;
        public RfqPortalQuoteRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<RfqPortalQuote>> GetByRfqIdAsync(int rfqId)
        {
            return await _context.Set<RfqPortalQuote>()
                .Include(q => q.QuoteItems)
                .AsNoTracking()
                .Where(q => q.RfqId == rfqId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }
        public async Task<IEnumerable<RfqPortalQuote>> GetBySupplierIdAsync(int supplierOrgId)
        {
            return await _context.Set<RfqPortalQuote>()
                .Include(q => q.QuoteItems)
                .AsNoTracking()
                .Where(q => q.SupplierOrganizationId == supplierOrgId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }
        public async Task<RfqPortalQuote?> GetByIdAsync(int id)
        {
            return await _context.Set<RfqPortalQuote>()
                .Include(q => q.QuoteItems)
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        }
        public async Task<RfqPortalQuote> AddAsync(RfqPortalQuote entity)
        {
            await _context.Set<RfqPortalQuote>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(RfqPortalQuote entity)
        {
            _context.Set<RfqPortalQuote>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RfqPortalQuote entity)
        {
            _context.Set<RfqPortalQuote>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<RfqPortalQuote>()
                .AnyAsync(q => q.QuoteId == id);
        }


    }
}
