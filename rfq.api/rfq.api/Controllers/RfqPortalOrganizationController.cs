using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rfq.api.DTOs;
using rfq.api.Services.Interfaces;

namespace rfq.api.Controllers
{
    [Route("api/rfq/organizations")]
    [ApiController]
    public class RfqPortalOrganizationController : ControllerBase
    {
        private readonly IRfqPortalOrganizationService _organizationService;

        public RfqPortalOrganizationController(IRfqPortalOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterOrganizationResponseDto>>> Register([FromBody] CreateRfqPortalOrganizationDto createDto)
        {
            var response = await _organizationService.RegisterAsync(createDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
