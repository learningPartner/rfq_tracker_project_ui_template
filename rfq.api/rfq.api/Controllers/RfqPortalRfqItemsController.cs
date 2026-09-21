using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/rfq-items")]
    [ApiController]
    public class RfqPortalRfqItemsController : ControllerBase
    {
        private readonly IRfqPortalRfqItemRepositoryService _service;

        public RfqPortalRfqItemsController(IRfqPortalRfqItemRepositoryService service)
        {
            _service = service;
        }
        [HttpGet("rfq/{rfqId:int}")]
        [EndpointSummary("Get all line items for a specific RFQ")]
        public async Task<IActionResult> GetByRfqId(int rfqId)
        {
            var response = await _service.GetItemsByRfqIdAsync(rfqId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        [EndpointSummary("Get details of a single RFQ line item by ID")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetItemByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        [EndpointSummary("Create a new line item for an RFQ")]
        public async Task<IActionResult> Create([FromBody] CreateRfqItemDto dto)
        {
            var response = await _service.CreateItemAsync(dto);
            return response.Success
                ? CreatedAtAction(nameof(GetById), new { id = response.Data?.RfqItemId }, response)
                : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Update an existing RFQ line item")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRfqItemDto dto)
        {
            var response = await _service.UpdateItemAsync(id, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Delete an RFQ line item by ID")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteItemAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
