using rfq.api.Constants;
using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services;

public class RfqPortalRfqItemService : IRfqPortalRfqItemService
{
    private readonly IRfqPortalRfqItemRepository _repository;

    public RfqPortalRfqItemService(IRfqPortalRfqItemRepository repository)
     {
        _repository = repository;
    }

    public async Task<ApiResponse<RfqPortalRfqItemDto>> GetByIdAsync(int rfqItemId)
    {
        var item = await _repository.GetByIdAsync(rfqItemId);

        if (item == null)
        {
            return ApiResponse<RfqPortalRfqItemDto>
                .FailureResponse(MessageConstants.RFQItemNotFound);
        }

        var itemDto = MapToDto(item);

        return ApiResponse<RfqPortalRfqItemDto>
            .SuccessResponse(
                itemDto,
                MessageConstants.OperationSuccessful
            );
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalRfqItemDto>>> GetByRfqIdAsync(int rfqId)
    {


    var items = await _repository.GetByRfqIdAsync(rfqId);

        var itemDtos = items.Select(MapToDto);

        return ApiResponse<IEnumerable<RfqPortalRfqItemDto>>
            .SuccessResponse(
                itemDtos,
                MessageConstants.OperationSuccessful
            );
    }

    public async Task<ApiResponse<RfqPortalRfqItemDto>> CreateAsync(
        int rfqId,
        CreateRfqPortalRfqItemDto createDto)
    {
        var item = new RfqPortalRfqItem
        {
            RfqId = rfqId,
            LineNumber = createDto.LineNumber,
            ProductCode = createDto.ProductCode,
          ProductName = createDto.ProductName,
            Material = createDto.Material,
            Quantity = createDto.Quantity,
            Unit = createDto.Unit,
           RequiredDate = createDto.RequiredDate,
            Specifications = createDto.Specifications,
            CreatedAt = DateTime.UtcNow
        };

        var createdItem = await _repository.AddAsync(item);

        var itemDto = MapToDto(createdItem);

        return ApiResponse<RfqPortalRfqItemDto>
            .SuccessResponse(
                itemDto,
                MessageConstants.RFQItemCreatedSuccessfully
            );
    }

    public async Task<ApiResponse<RfqPortalRfqItemDto>> UpdateAsync(
        int rfqId,
        int rfqItemId,
        UpdateRfqPortalRfqItemDto updateDto)
    {
        var item = await _repository.GetByIdAsync(rfqItemId);

        if (item == null) { 
            return ApiResponse<RfqPortalRfqItemDto>
                .FailureResponse(MessageConstants.RFQItemNotFound);
        }

        if (item.RfqId != rfqId)
        {
            return ApiResponse<RfqPortalRfqItemDto>
                .FailureResponse(MessageConstants.InvalidRequest);
        }

        item.LineNumber = updateDto.LineNumber;
        item.ProductCode = updateDto.ProductCode;
        item.ProductName = updateDto.ProductName;
          item.Material = updateDto.Material;
        item.Quantity = updateDto.Quantity;
        item.Unit = updateDto.Unit;
        item.RequiredDate = updateDto.RequiredDate;
        item.Specifications = updateDto.Specifications;

        await _repository.UpdateAsync(item);

        var itemDto = MapToDto(item);

        return ApiResponse<RfqPortalRfqItemDto>
            .SuccessResponse(
                itemDto,
                MessageConstants.RFQItemUpdatedSuccessfully
            );
    }

    public async Task<ApiResponse<bool>> DeleteAsync(
        int rfqId,
        int rfqItemId)
    {
        var item = await _repository.GetByIdAsync(rfqItemId);

        if (item == null)
        {
            return ApiResponse<bool>
                .FailureResponse(MessageConstants.RFQItemNotFound);
        }

        if (item.RfqId != rfqId)
        {
            return ApiResponse<bool>
                .FailureResponse(MessageConstants.InvalidRequest);
        }

        await _repository.DeleteAsync(item);

        return ApiResponse<bool>
            .SuccessResponse(
                true,
                MessageConstants.RFQItemDeletedSuccessfully
            );
    }

    private static RfqPortalRfqItemDto MapToDto(RfqPortalRfqItem item)
    {
        return new RfqPortalRfqItemDto
        {
            RfqItemId = item.RfqItemId,
            RfqId = item.RfqId,
            LineNumber = item.LineNumber,
            ProductCode = item.ProductCode,
            ProductName = item.ProductName,
            Material = item.Material,
            Quantity = item.Quantity,
            Unit = item.Unit,
            RequiredDate = item.RequiredDate,
            Specifications = item.Specifications,
            CreatedAt = item.CreatedAt
        };
    }
}