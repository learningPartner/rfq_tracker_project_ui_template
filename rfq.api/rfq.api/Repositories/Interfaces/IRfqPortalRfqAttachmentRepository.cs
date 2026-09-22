using System.Collections;
using rfq.api.Entities;

namespace rfq.api.Repositories.Interfaces
{
    public interface IRfqPortalRfqAttachmentRepository
    {
        Task<IEnumerable<RfqPortalRfqAttachment>> GetByRfqIdAsync(int rfqId);
        Task<RfqPortalRfqAttachment?> GetByIdAsync(int attachmentId);
        Task<RfqPortalRfqAttachment> CreateAsync(RfqPortalRfqAttachment attachment);
        Task<bool> DeleteAsync(int attachmentId);
    }
}
