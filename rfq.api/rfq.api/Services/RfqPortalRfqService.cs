using rfq.api.Constants;
using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services;

public class RfqPortalRfqService : IRfqPortalRfqService
{
    private readonly IRfqPortalRfqRepository _repository;

    public RfqPortalRfqService(IRfqPortalRfqRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<RfqPortalRfqDto>> GetByIdAsync(int rfqId)
    {
        var rfq = await _repository.GetByIdAsync(rfqId);
        if (rfq == null)
        {
            return ApiResponse<RfqPortalRfqDto>.FailureResponse(MessageConstants.RFQNotFound);
        }

        var rfqDto = MapToDto(rfq);
        return ApiResponse<RfqPortalRfqDto>.SuccessResponse(rfqDto, MessageConstants.RFQRetrievedSuccessfully);
    }

    public async Task<ApiResponse<PaginatedResult<RfqPortalRfqDto>>> GetAllAsync(string? status = null, string? industry = null, string? category = null, int page = 1, int pageSize = 10)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var (items, totalCount) = await _repository.GetFilteredAsync(status, industry, category, page, pageSize);

        var rfqDtos = items.Select(MapToDto).ToList();

        var result = new PaginatedResult<RfqPortalRfqDto>
        {
            Items = rfqDtos,
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        return ApiResponse<PaginatedResult<RfqPortalRfqDto>>.SuccessResponse(result, MessageConstants.RFQsRetrievedSuccessfully);
    }

    public async Task<ApiResponse<RfqPortalRfqDto>> GetByRfqNumberAsync(string rfqNumber)
    {
        var rfq = await _repository.GetByRfqNumberAsync(rfqNumber);
        if (rfq == null)
        {
            return ApiResponse<RfqPortalRfqDto>.FailureResponse(MessageConstants.RFQNotFound);
        }

        var rfqDto = MapToDto(rfq);
        return ApiResponse<RfqPortalRfqDto>.SuccessResponse(rfqDto, MessageConstants.RFQRetrievedSuccessfully);
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByClientOrganizationIdAsync(int clientOrganizationId)
    {
        var rfqs = await _repository.GetByClientOrganizationIdAsync(clientOrganizationId);
        var rfqDtos = rfqs.Select(MapToDto);
        return ApiResponse<IEnumerable<RfqPortalRfqDto>>.SuccessResponse(rfqDtos, MessageConstants.RFQsRetrievedSuccessfully);
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByStatusAsync(string status)
    {
        var rfqs = await _repository.GetByStatusAsync(status);
        var rfqDtos = rfqs.Select(MapToDto);
        return ApiResponse<IEnumerable<RfqPortalRfqDto>>.SuccessResponse(rfqDtos, MessageConstants.RFQsRetrievedSuccessfully);
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByIndustryAsync(string industry)
    {
        var rfqs = await _repository.GetByIndustryAsync(industry);
        var rfqDtos = rfqs.Select(MapToDto);
        return ApiResponse<IEnumerable<RfqPortalRfqDto>>.SuccessResponse(rfqDtos, MessageConstants.RFQsRetrievedSuccessfully);
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalRfqDto>>> GetByCategoryAsync(string category)
    {
        var rfqs = await _repository.GetByCategoryAsync(category);
        var rfqDtos = rfqs.Select(MapToDto);
        return ApiResponse<IEnumerable<RfqPortalRfqDto>>.SuccessResponse(rfqDtos, MessageConstants.RFQsRetrievedSuccessfully);
    }

    public async Task<ApiResponse<RfqPortalRfqDto>> CreateAsync(CreateRfqPortalRfqDto createDto)
    {
        // Check if RFQ number already exists
        if (await _repository.RfqNumberExistsAsync(createDto.RfqNumber))
        {
            return ApiResponse<RfqPortalRfqDto>.FailureResponse($"RFQ number '{createDto.RfqNumber}' already exists.");
        }

        var rfq = new RfqPortalRfq
        {
            RfqNumber = createDto.RfqNumber,
            ClientOrganizationId = createDto.ClientOrganizationId,
            Title = createDto.Title,
            Description = createDto.Description,
            Industry = createDto.Industry,
            Category = createDto.Category,
            ManufacturingProcess = createDto.ManufacturingProcess,
            Material = createDto.Material,
            LocationCity = createDto.LocationCity,
            LocationState = createDto.LocationState,
            ResponseDeadline = createDto.ResponseDeadline,
            RfqStatus = createDto.RfqStatus ?? "Draft",
            PublishedDate = createDto.PublishedDate,
            CreatedByUserId = createDto.CreatedByUserId,
            AwardedQuoteId = createDto.AwardedQuoteId,
            CreatedAt = DateTime.UtcNow
        };

        var createdRfq = await _repository.AddAsync(rfq);
        var rfqDto = MapToDto(createdRfq);
        return ApiResponse<RfqPortalRfqDto>.SuccessResponse(rfqDto, MessageConstants.RFQCreatedSuccessfully);
    }

    public async Task<ApiResponse<RfqPortalRfqDto>> UpdateAsync(UpdateRfqPortalRfqDto updateDto)
    {
        var rfq = await _repository.GetByIdAsync(updateDto.RfqId);
        if (rfq == null)
        {
            return ApiResponse<RfqPortalRfqDto>.FailureResponse(MessageConstants.RFQNotFound);
        }

        // Check if RFQ number already exists for another RFQ
        if (await _repository.RfqNumberExistsAsync(updateDto.RfqNumber, updateDto.RfqId))
        {
            return ApiResponse<RfqPortalRfqDto>.FailureResponse($"RFQ number '{updateDto.RfqNumber}' already exists.");
        }

        rfq.RfqNumber = updateDto.RfqNumber;
        rfq.ClientOrganizationId = updateDto.ClientOrganizationId;
        rfq.Title = updateDto.Title;
        rfq.Description = updateDto.Description;
        rfq.Industry = updateDto.Industry;
        rfq.Category = updateDto.Category;
        rfq.ManufacturingProcess = updateDto.ManufacturingProcess;
        rfq.Material = updateDto.Material;
        rfq.LocationCity = updateDto.LocationCity;
        rfq.LocationState = updateDto.LocationState;
        rfq.ResponseDeadline = updateDto.ResponseDeadline;
        rfq.RfqStatus = updateDto.RfqStatus;
        rfq.PublishedDate = updateDto.PublishedDate;
        rfq.CreatedByUserId = updateDto.CreatedByUserId;
        rfq.AwardedQuoteId = updateDto.AwardedQuoteId;

        await _repository.UpdateAsync(rfq);
        var rfqDto = MapToDto(rfq);
        return ApiResponse<RfqPortalRfqDto>.SuccessResponse(rfqDto, MessageConstants.RFQUpdatedSuccessfully);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int rfqId)
    {
        var rfq = await _repository.GetByIdAsync(rfqId);
        if (rfq == null)
        {
            return ApiResponse<bool>.FailureResponse(MessageConstants.RFQNotFound);
        }

        await _repository.DeleteAsync(rfq);
        return ApiResponse<bool>.SuccessResponse(true, MessageConstants.RFQDeletedSuccessfully);
    }

    private static RfqPortalRfqDto MapToDto(RfqPortalRfq rfq)
    {
        return new RfqPortalRfqDto
        {
            RfqId = rfq.RfqId,
            RfqNumber = rfq.RfqNumber,
            ClientOrganizationId = rfq.ClientOrganizationId,
            Title = rfq.Title,
            Description = rfq.Description,
            Industry = rfq.Industry,
            Category = rfq.Category,
            ManufacturingProcess = rfq.ManufacturingProcess,
            Material = rfq.Material,
            LocationCity = rfq.LocationCity,
            LocationState = rfq.LocationState,
            ResponseDeadline = rfq.ResponseDeadline,
            RfqStatus = rfq.RfqStatus,
            PublishedDate = rfq.PublishedDate,
            CreatedByUserId = rfq.CreatedByUserId,
            AwardedQuoteId = rfq.AwardedQuoteId,
            CreatedAt = rfq.CreatedAt,
            UpdatedAt = rfq.UpdatedAt
        };
    }
}
