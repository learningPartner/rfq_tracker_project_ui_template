using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers;

[ApiController]
[Route("api/rfq/rfqs/{rfqId}/items")]
public class RFQItemController : ControllerBase
{
    private readonly IRfqPortalRfqItemService _itemService;

    public RFQItemController(IRfqPortalRfqItemService itemService)
    {
        _itemService = itemService;
    }

    // GET: api/rfq/rfqs/142/items
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqItemDto>>>> GetByRfqId(int rfqId)
    {
        var response = await _itemService.GetByRfqIdAsync(rfqId);

        return Ok(response);
    }

    // GET: api/rfq/rfqs/142/items/5
    [HttpGet("{rfqItemId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqItemDto>>> GetById(
        int rfqId,
        int rfqItemId)
    {
        var response = await _itemService.GetByIdAsync(rfqItemId);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    // POST: api/rfq/rfqs/142/items
    [HttpPost]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqItemDto>>> Create(
        int rfqId,
        [FromBody] CreateRfqPortalRfqItemDto createDto)
    {
        var response = await _itemService.CreateAsync(rfqId, createDto);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                rfqId = rfqId,
                rfqItemId = response.Data?.RfqItemId
            },
            response
        );
    }

    // PUT: api/rfq/rfqs/142/items/5
    [HttpPut("{rfqItemId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqItemDto>>> Update(
        int rfqId,
        int rfqItemId,
        [FromBody] UpdateRfqPortalRfqItemDto updateDto)
    {
        var response = await _itemService.UpdateAsync(
            rfqId,
            rfqItemId,
            updateDto
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // DELETE: api/rfq/rfqs/142/items/5
    [HttpDelete("{rfqItemId}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(
        int rfqId,
        int rfqItemId)
    {
        var response = await _itemService.DeleteAsync(
            rfqId,
            rfqItemId
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}