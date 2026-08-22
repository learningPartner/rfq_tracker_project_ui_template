using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers;

[ApiController]
[Route("api/rfq/rfqs")]
public class RFQController : ControllerBase
{
    private readonly IRfqPortalRfqService _rfqService;

    public RFQController(IRfqPortalRfqService rfqService)
    {
        _rfqService = rfqService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqDto>>>> GetAll()
    {
        var response = await _rfqService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{rfqId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqDto>>> GetById(int rfqId)
    {
        var response = await _rfqService.GetByIdAsync(rfqId);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("number/{rfqNumber}")]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqDto>>> GetByRfqNumber(string rfqNumber)
    {
        var response = await _rfqService.GetByRfqNumberAsync(rfqNumber);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("client-organization/{clientOrganizationId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqDto>>>> GetByClientOrganization(int clientOrganizationId)
    {
        var response = await _rfqService.GetByClientOrganizationIdAsync(clientOrganizationId);
        return Ok(response);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqDto>>>> GetByStatus(string status)
    {
        var response = await _rfqService.GetByStatusAsync(status);
        return Ok(response);
    }

    [HttpGet("industry/{industry}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqDto>>>> GetByIndustry(string industry)
    {
        var response = await _rfqService.GetByIndustryAsync(industry);
        return Ok(response);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalRfqDto>>>> GetByCategory(string category)
    {
        var response = await _rfqService.GetByCategoryAsync(category);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqDto>>> Create([FromBody] CreateRfqPortalRfqDto createDto)
    {
        var response = await _rfqService.CreateAsync(createDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return CreatedAtAction(nameof(GetById), new { rfqId = response.Data?.RfqId }, response);
    }

    [HttpPut("{rfqId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalRfqDto>>> Update(int rfqId, [FromBody] UpdateRfqPortalRfqDto updateDto)
    {
        if (rfqId != updateDto.RfqId)
        {
            return BadRequest(new ApiResponse<RfqPortalRfqDto>
            {
                Success = false,
                Message = "RFQ ID mismatch"
            });
        }

        var response = await _rfqService.UpdateAsync(updateDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{rfqId}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int rfqId)
    {
        var response = await _rfqService.DeleteAsync(rfqId);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}
