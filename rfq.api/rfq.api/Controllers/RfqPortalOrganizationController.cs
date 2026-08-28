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

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<RfqPortalOrganizationDto>>>> GetAll()
        {
            var response = await _organizationService.GetAllAsync();
            return Ok(response);
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

        [HttpGet("{organizationId}")]
        public async Task<ActionResult<ApiResponse<RfqPortalOrganizationDto>>> GetById(int organizationId)
        {
            var response = await _organizationService.GetByIdAsync(organizationId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("{organizationId}")]
        public async Task<ActionResult<ApiResponse<RfqPortalOrganizationDto>>> Update(int organizationId, [FromBody] UpdateRfqPortalOrganizationDto updateDto)
        {
            if (organizationId != updateDto.OrganizationId)
            {
                return BadRequest(ApiResponse<RfqPortalOrganizationDto>.FailureResponse("Organization ID mismatch."));
            }

            var response = await _organizationService.UpdateAsync(updateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{organizationId}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int organizationId)
        {
            var response = await _organizationService.DeleteAsync(organizationId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
