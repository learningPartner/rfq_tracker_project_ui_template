using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services
{
    public class RfqPortalQuoteService:IRfqPortalQuoteService
    {
        private readonly IRfqPortalQuoteRepository _repository;

        public RfqPortalQuoteService(IRfqPortalQuoteRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<QuoteDto>> GetQuotesByRfqIdAsync(int rfqId)
        {
            var entities = await _repository.GetByRfqIdAsync(rfqId);
            return entities.Select(MapToDto);
        }

        public async Task<IEnumerable<QuoteDto>> GetQuotesBySupplierIdAsync(int supplierOrgId)
        {
            var entities = await _repository.GetBySupplierIdAsync(supplierOrgId);
            return entities.Select(MapToDto);
        }

        public async Task<QuoteDto?> GetQuoteByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<QuoteDto> CreateQuoteAsync(CreateQuoteDto dto)
        {
            var entity = new RfqPortalQuote
            {
                RfqId = dto.RfqId,
                SupplierOrganizationId = dto.SupplierOrganizationId,
                QuoteNumber = dto.QuoteNumber,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                ValidUntil = dto.ValidUntil,
                LeadTimeDays = dto.LeadTimeDays,
                PaymentTerms = dto.PaymentTerms,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Submitted" : dto.Status,
                Remarks = dto.Remarks,
                CreatedAt = DateTime.UtcNow,
                QuoteItems = dto.QuoteItems.Select(item => new RfqPortalQuoteItem
                {
                    RfqItemId = item.RfqItemId,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                    LeadTimeDays = item.LeadTimeDays,
                    Remarks = item.Remarks
                }).ToList()
            };

            var createdEntity = await _repository.AddAsync(entity);
            return MapToDto(createdEntity);
        }

        public async Task<QuoteDto?> UpdateQuoteStatusAsync(int id, UpdateQuoteStatusDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            entity.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.Remarks))
            {
                entity.Remarks = dto.Remarks;
            }

            await _repository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        public async Task<bool> DeleteQuoteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            await _repository.DeleteAsync(entity);
            return true;
        }

        private static QuoteDto MapToDto(RfqPortalQuote entity)
        {
            return new QuoteDto
            {
                QuoteId = entity.QuoteId,
                RfqId = entity.RfqId,
                SupplierOrganizationId = entity.SupplierOrganizationId,
                QuoteNumber = entity.QuoteNumber,
                TotalAmount = entity.TotalAmount,
                Currency = entity.Currency,
                ValidUntil = entity.ValidUntil,
                LeadTimeDays = entity.LeadTimeDays,
                PaymentTerms = entity.PaymentTerms,
                Status = entity.Status,
                Remarks = entity.Remarks,
                CreatedAt = entity.CreatedAt,
                QuoteItems = entity.QuoteItems?.Select(item => new QuoteItemDto
                {
                    QuoteItemId = item.QuoteItemId,
                    QuoteId = item.QuoteId,
                    RfqItemId = item.RfqItemId,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                    LeadTimeDays = item.LeadTimeDays,
                    Remarks = item.Remarks
                }).ToList() ?? new List<QuoteItemDto>()
            };
        }


        }
}
