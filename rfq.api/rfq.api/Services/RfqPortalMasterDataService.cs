using rfq.api.DTOs;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;
using rfq.api.Services.Interfaces;

namespace rfq.api.Services;

public class RfqPortalMasterDataService : IRfqPortalMasterDataService
{
    private readonly IRfqPortalMasterDataRepository _repository;

    public RfqPortalMasterDataService(IRfqPortalMasterDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetAllAsync()
    {
        try
        {
            var masterDatas = await _repository.GetAllAsync();
            var dtos = masterDatas.Select(m => new RfqPortalMasterDataDto
            {
                MasterDataId = m.MasterDataId,
                Type = m.Type,
                Value = m.Value
            }).ToList();

            return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>.SuccessResponse(
                dtos,
                "Retrieved successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>.FailureResponse(
                "An error occurred while retrieving master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<RfqPortalMasterDataDto>> GetByIdAsync(int masterDataId)
    {
        try
        {
            var masterData = await _repository.GetByIdAsync(masterDataId);
            if (masterData == null)
            {
                return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                    "Master data not found."
                );
            }

            var dto = new RfqPortalMasterDataDto
            {
                MasterDataId = masterData.MasterDataId,
                Type = masterData.Type,
                Value = masterData.Value
            };

            return ApiResponse<RfqPortalMasterDataDto>.SuccessResponse(
                dto,
                "Retrieved successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                "An error occurred while retrieving master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetByTypeAsync(string type)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>.FailureResponse(
                    "Type is required."
                );
            }

            var masterDatas = await _repository.GetByTypeAsync(type);
            var dtos = masterDatas.Select(m => new RfqPortalMasterDataDto
            {
                MasterDataId = m.MasterDataId,
                Type = m.Type,
                Value = m.Value
            }).ToList();

            return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>.SuccessResponse(
                dtos,
                "Retrieved successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>.FailureResponse(
                "An error occurred while retrieving master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<RfqPortalMasterDataDto>> CreateAsync(CreateRfqPortalMasterDataDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Type) || string.IsNullOrWhiteSpace(dto.Value))
            {
                return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                    "Type and Value are required.",
                    new List<string> { "Type is required", "Value is required" }
                );
            }

            var masterData = new RfqPortalMasterData
            {
                Type = dto.Type,
                Value = dto.Value
            };

            var createdMasterData = await _repository.AddAsync(masterData);

            var responseDto = new RfqPortalMasterDataDto
            {
                MasterDataId = createdMasterData.MasterDataId,
                Type = createdMasterData.Type,
                Value = createdMasterData.Value
            };

            return ApiResponse<RfqPortalMasterDataDto>.SuccessResponse(
                responseDto,
                "Created successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                "An error occurred while creating master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<RfqPortalMasterDataDto>> UpdateAsync(UpdateRfqPortalMasterDataDto dto)
    {
        try
        {
            var masterData = await _repository.GetByIdAsync(dto.MasterDataId);
            if (masterData == null)
            {
                return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                    "Master data not found."
                );
            }

            if (string.IsNullOrWhiteSpace(dto.Type) || string.IsNullOrWhiteSpace(dto.Value))
            {
                return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                    "Type and Value are required."
                );
            }

            masterData.Type = dto.Type;
            masterData.Value = dto.Value;

            await _repository.UpdateAsync(masterData);

            var responseDto = new RfqPortalMasterDataDto
            {
                MasterDataId = masterData.MasterDataId,
                Type = masterData.Type,
                Value = masterData.Value
            };

            return ApiResponse<RfqPortalMasterDataDto>.SuccessResponse(
                responseDto,
                "Updated successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<RfqPortalMasterDataDto>.FailureResponse(
                "An error occurred while updating master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int masterDataId)
    {
        try
        {
            var masterData = await _repository.GetByIdAsync(masterDataId);
            if (masterData == null)
            {
                return ApiResponse<bool>.FailureResponse(
                    "Master data not found."
                );
            }

            await _repository.DeleteAsync(masterDataId);

            return ApiResponse<bool>.SuccessResponse(
                true,
                "Deleted successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<bool>.FailureResponse(
                "An error occurred while deleting master data.",
                new List<string> { ex.Message }
            );
        }
    }

    public async Task<ApiResponse<List<string>>> GetAllTypesAsync()
    {
        try
        {
            var masterDatas = await _repository.GetAllAsync();
            var types = masterDatas.Select(m => m.Type).Distinct().ToList();

            return ApiResponse<List<string>>.SuccessResponse(
                types,
                "Retrieved successfully."
            );
        }
        catch (Exception ex)
        {
            return ApiResponse<List<string>>.FailureResponse(
                "An error occurred while retrieving types.",
                new List<string> { ex.Message }
            );
        }
    }
}
