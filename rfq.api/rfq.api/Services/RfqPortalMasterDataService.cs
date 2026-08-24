using rfq.api.Constants;
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

    public async Task<ApiResponse<IEnumerable<RfqPortalMasterDataDto>>> GetByTypeAsync(string type)
    {
        var masterData = await _repository.GetByTypeAsync(type);

        if (!masterData.Any())
        {
            return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>
                .FailureResponse(MessageConstants.MasterDataNotFound);
        }

        var masterDataDtos = masterData.Select(MapToDto);

        return ApiResponse<IEnumerable<RfqPortalMasterDataDto>>
            .SuccessResponse(
                masterDataDtos,
                MessageConstants.MasterDataRetrievedSuccessfully);
    }

    private static RfqPortalMasterDataDto MapToDto(RfqPortalMasterData masterData)
    {
        return new RfqPortalMasterDataDto
        {
            MasterDataId = masterData.MasterDataId,
            Value = masterData.Value
        };
    }
}