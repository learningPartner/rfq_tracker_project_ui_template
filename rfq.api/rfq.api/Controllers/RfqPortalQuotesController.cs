using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RfqPortalQuotesController : ControllerBase
    {
        private readonly IRfqPortalQuoteService _quoteService;
        public RfqPortalQuotesController(IRfqPortalQuoteService quoteService)
        {
            _quoteService = quoteService;
        }
        [HttpGet("rfq/{rfqId}")]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> GetQuotesByRfqId(int rfqId)
        {
            var quotes = await _quoteService.GetQuotesByRfqIdAsync(rfqId);
            return Ok(quotes);
        }

        [HttpGet("supplier/{supplierOrgId}")]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> GetQuotesBySupplierId(int supplierOrgId)
        {
            var quotes = await _quoteService.GetQuotesBySupplierIdAsync(supplierOrgId);
            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteDto>> GetQuoteById(int id)
        {
            var quote = await _quoteService.GetQuoteByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        [HttpPost]
        public async Task<ActionResult<QuoteDto>> CreateQuote([FromBody] CreateQuoteDto dto)
        {
            var createdQuote = await _quoteService.CreateQuoteAsync(dto);

            return CreatedAtAction(nameof(GetQuoteById), new { id = createdQuote.QuoteId }, createdQuote);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<QuoteDto>> UpdateQuoteStatus(int id, [FromBody] UpdateQuoteStatusDto dto)
        {
            var updatedQuote = await _quoteService.UpdateQuoteStatusAsync(id, dto);

            if (updatedQuote == null)
            {
                return NotFound();
            }

            return Ok(updatedQuote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var success = await _quoteService.DeleteQuoteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
