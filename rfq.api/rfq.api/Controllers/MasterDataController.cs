using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers;

[ApiController]
[Route("api/rfq/master-data")]
public class MasterDataController : ControllerBase
{
    private readonly IRfqPortalMasterDataService _masterDataService;

    public MasterDataController(IRfqPortalMasterDataService masterDataService)
    {
        _masterDataService = masterDataService;
    }

    [HttpGet("{type}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>>> GetByType(string type)
    {
        var response = await _masterDataService.GetByTypeAsync(type);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}