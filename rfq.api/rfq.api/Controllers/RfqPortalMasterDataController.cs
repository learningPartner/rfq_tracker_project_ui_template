using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers;

[Route("api/rfq/master-data")]
[ApiController]
public class RfqPortalMasterDataController : ControllerBase
{
    private readonly IRfqPortalMasterDataService _masterDataService;

    public RfqPortalMasterDataController(IRfqPortalMasterDataService masterDataService)
    {
        _masterDataService = masterDataService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>>> GetAll()
    {
        var response = await _masterDataService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{masterDataId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalMasterDataDto>>> GetById(int masterDataId)
    {
        var response = await _masterDataService.GetByIdAsync(masterDataId);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("by-type/{type}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>>> GetByType(string type)
    {
        var response = await _masterDataService.GetByTypeAsync(type);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("types/all")]
    public async Task<ActionResult<ApiResponse<List<string>>>> GetAllTypes()
    {
        var response = await _masterDataService.GetAllTypesAsync();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<RfqPortalMasterDataDto>>> Create(
        [FromBody] CreateRfqPortalMasterDataDto createDto)
    {
        var response = await _masterDataService.CreateAsync(createDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return CreatedAtAction(
            nameof(GetById),
            new { masterDataId = response.Data?.MasterDataId },
            response
        );
    }

    [HttpPut("{masterDataId}")]
    public async Task<ActionResult<ApiResponse<RfqPortalMasterDataDto>>> Update(
        int masterDataId,
        [FromBody] UpdateRfqPortalMasterDataDto updateDto)
    {
        if (masterDataId != updateDto.MasterDataId)
        {
            return BadRequest(ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                "Master Data ID mismatch."
            ));
        }

        var response = await _masterDataService.UpdateAsync(updateDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{masterDataId}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int masterDataId)
    {
        var response = await _masterDataService.DeleteAsync(masterDataId);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}
