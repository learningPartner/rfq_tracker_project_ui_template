using Microsoft.EntityFrameworkCore;
using rfq.api.Data;
using rfq.api.Entities;
using rfq.api.Repositories.Interfaces;

namespace rfq.api.Repositories;

public class RfqPortalMasterDataRepository : IRfqPortalMasterDataRepository
{
    private readonly ApplicationDbContext _context;

    public RfqPortalMasterDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RfqPortalMasterData>> GetByTypeAsync(string type) // not 4 separate api but 1 api
    {
        return await _context.RfqPortalMasterData
            .Where(x => x.Type == type)
            .OrderBy(x => x.MasterDataId)
            .ToListAsync();
    }
}