using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services
{
    public class RfqPortalRfqItemService: IRfqPortalRfqItemRepositoryService
    {
        private readonly IRfqPortalRfqItemRepository _repository;

        public RfqPortalRfqItemService(IRfqPortalRfqItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<IEnumerable<RfqItemDto>>> GetItemsByRfqIdAsync(int rfqId)
        {
            try
            {
                var entities = await _repository.GetByRfqIdAsync(rfqId);
                var dtos = entities.Select(MapToDto);
                return new ApiResponse<IEnumerable<RfqItemDto>>
                {
                    Success = true,
                    Message = "RFQ items retrieved successfully.",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<RfqItemDto>>
                {
                    Success = false,
                    Message = $"Error retrieving RFQ items: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<RfqItemDto>> GetItemByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ApiResponse<RfqItemDto>
                {
                    Success = false,
                    Message = "RFQ line item not found.",
                    Data = null
                };
            }

            return new ApiResponse<RfqItemDto>
            {
                Success = true,
                Message = "RFQ item retrieved successfully.",
                Data = MapToDto(entity)
            };
        }

        public async Task<ApiResponse<RfqItemDto>> CreateItemAsync(CreateRfqItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProductName) || dto.Quantity <= 0)
            {
                return new ApiResponse<RfqItemDto>
                {
                    Success = false,
                    Message = "Product name and a positive quantity are required.",
                    Data = null
                };
            }

            var entity = new RfqPortalRfqItem
            {
                RfqId = dto.RfqId,
                LineNumber = dto.LineNumber,
                ProductCode = dto.ProductCode,
                ProductName = dto.ProductName,
                Material = dto.Material,
                Quantity = dto.Quantity,
                Unit = dto.Unit,
                RequiredDate = dto.RequiredDate,
                Specifications = dto.Specifications,
                CreatedAt = DateTime.UtcNow
            };

            var createdEntity = await _repository.AddAsync(entity);
            return new ApiResponse<RfqItemDto>
            {
                Success = true,
                Message = "RFQ line item created successfully.",
                Data = MapToDto(createdEntity)
            };
        }

        public async Task<ApiResponse<RfqItemDto>> UpdateItemAsync(int id, UpdateRfqItemDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return new ApiResponse<RfqItemDto>
                {
                    Success = false,
                    Message = "RFQ line item not found.",
                    Data = null
                };
            }

            existing.LineNumber = dto.LineNumber;
            existing.ProductCode = dto.ProductCode;
            existing.ProductName = dto.ProductName;
            existing.Material = dto.Material;
            existing.Quantity = dto.Quantity;
            existing.Unit = dto.Unit;
            existing.RequiredDate = dto.RequiredDate;
            existing.Specifications = dto.Specifications;

            await _repository.UpdateAsync(existing);
            return new ApiResponse<RfqItemDto>
            {
                Success = true,
                Message = "RFQ line item updated successfully.",
                Data = MapToDto(existing)
            };
        }

        public async Task<ApiResponse<bool>> DeleteItemAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "RFQ line item not found to delete.",
                    Data = false
                };
            }

            await _repository.DeleteAsync(existing);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "RFQ line item deleted successfully.",
                Data = true
            };
        }

        private static RfqItemDto MapToDto(RfqPortalRfqItem entity) => new()
        {
            RfqItemId = entity.RfqItemId,
            RfqId = entity.RfqId,
            LineNumber = entity.LineNumber,
            ProductCode = entity.ProductCode,
            ProductName = entity.ProductName,
            Material = entity.Material,
            Quantity = entity.Quantity,
            Unit = entity.Unit,
            RequiredDate = entity.RequiredDate,
            Specifications = entity.Specifications,
            CreatedAt = entity.CreatedAt
        };
    }
}
