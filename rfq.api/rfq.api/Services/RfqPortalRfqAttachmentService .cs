using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services
{
    public class RfqPortalRfqAttachmentService: IRfqPortalRfqAttachmentService
    {
        private readonly IRfqPortalRfqAttachmentRepository _repository;

        public RfqPortalRfqAttachmentService(IRfqPortalRfqAttachmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RfqAttachmentDtos>> GetByRfqIdAsync(int rfqId)
        {
            var entities = await _repository.GetByRfqIdAsync(rfqId);

            List<RfqAttachmentDtos> result = new List<RfqAttachmentDtos>();
            foreach (var entity in entities)
            {
                result.Add(MapToDto(entity));
            }

            return result;
        }

        public async Task<RfqAttachmentDtos?> GetByIdAsync(int attachmentId)
        {
            var entity = await _repository.GetByIdAsync(attachmentId);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<RfqAttachmentDtos> CreateAsync(CreateRfqAttachmentDto dto)
        {
            var entity = new RfqPortalRfqAttachment
            {
                RfqId = dto.RfqId,
                RfqItemId = dto.RfqItemId,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                FileType = dto.FileType,
                FileSizeBytes = dto.FileSizeBytes,
                UploadedByUserId = dto.UploadedByUserId,
                UploadedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(entity);
            return MapToDto(created);
        }

        public async Task<bool> DeleteAsync(int attachmentId)
        {
            return await _repository.DeleteAsync(attachmentId);
        }

        private RfqAttachmentDtos MapToDto(RfqPortalRfqAttachment entity)
        {
            return new RfqAttachmentDtos
            {
                AttachmentId = entity.AttachmentId,
                RfqId = entity.RfqId,
                RfqItemId = entity.RfqItemId,
                FileName = entity.FileName,
                FilePath = entity.FilePath,
                FileType = entity.FileType,
                FileSizeBytes = entity.FileSizeBytes,
                UploadedByUserId = entity.UploadedByUserId,
                UploadedAt = entity.UploadedAt
            };
        }

    }
}
