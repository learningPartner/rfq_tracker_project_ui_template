using rfq.api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rfq.api.Services.Interfaces
{
    public interface IRfqPortalQuoteService
    {
        Task<IEnumerable<QuoteDto>> GetQuotesByRfqIdAsync(int rfqId);
        Task<IEnumerable<QuoteDto>> GetQuotesBySupplierIdAsync(int supplierOrgId);
        Task<QuoteDto?> GetQuoteByIdAsync(int id);
        Task<QuoteDto> CreateQuoteAsync(CreateQuoteDto dto);
        Task<QuoteDto?> UpdateQuoteStatusAsync(int id, UpdateQuoteStatusDto dto);
        Task<bool> DeleteQuoteAsync(int id);
    }
}
