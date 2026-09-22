using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories
{
    public class RfqPortalRfqAttachmentRepository : IRfqPortalRfqAttachmentRepository
    {
        private readonly ApplicationDbContext _context;

        public RfqPortalRfqAttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RfqPortalRfqAttachment>> GetByRfqIdAsync(int rfqId)
        {
            return await _context.RfqPortalRfqAttachments
                .Where(a => a.RfqId == rfqId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RfqPortalRfqAttachment?> GetByIdAsync(int attachmentId)
        {
            return await _context.RfqPortalRfqAttachments
                 .AsNoTracking()
                 .FirstOrDefaultAsync(a => a.AttachmentId == attachmentId);
        }

        public async Task<RfqPortalRfqAttachment> CreateAsync(RfqPortalRfqAttachment attachment)
        {
            await _context.RfqPortalRfqAttachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
            return attachment;
        }

        public async Task<bool> DeleteAsync(int attachmentId)
        {
            var entity = await _context.RfqPortalRfqAttachments.FindAsync(attachmentId);
            if (entity == null) return false;

            _context.RfqPortalRfqAttachments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}