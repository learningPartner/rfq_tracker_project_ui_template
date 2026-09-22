using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RfqPortalRfqAttachmentsController : ControllerBase
    {
        private readonly IRfqPortalRfqAttachmentService _service;

        public RfqPortalRfqAttachmentsController(IRfqPortalRfqAttachmentService service)
        {
            _service = service;
        }
        [HttpGet("rfq/{rfqId}")]
        [EndpointSummary("  Get all attachments for a specific RFQ.")]
        public async Task<ActionResult<IEnumerable<RfqAttachmentDtos>>> GetByRfqId(int rfqId)
        {
            var result = await _service.GetByRfqIdAsync(rfqId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [EndpointSummary("Get a single attachment by its ID.")]

        public async Task<ActionResult<RfqAttachmentDtos>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [EndpointSummary("Upload/create a new attachment record.")]
        public async Task<ActionResult<RfqAttachmentDtos>> Create([FromBody] CreateRfqAttachmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AttachmentId }, created);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Delete an attachment by its ID.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
