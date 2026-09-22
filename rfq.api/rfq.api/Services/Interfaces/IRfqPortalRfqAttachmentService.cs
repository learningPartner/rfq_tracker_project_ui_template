using rfq.api.DTOs;

namespace rfq.api.Services.Interfaces
{
    public interface IRfqPortalRfqAttachmentService
    {
        Task<IEnumerable<RfqAttachmentDtos>> GetByRfqIdAsync(int rfqId);
        Task<RfqAttachmentDtos?> GetByIdAsync(int attachmentId);
        Task<RfqAttachmentDtos> CreateAsync(CreateRfqAttachmentDto dto);
        Task<bool> DeleteAsync(int attachmentId);
    }
}
