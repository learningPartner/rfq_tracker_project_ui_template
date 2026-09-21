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

    public async Task<IEnumerable<RfqPortalMasterData>> GetAllAsync()
    {
        return await _context.RfqPortalMasterDatas.ToListAsync();
    }

    public async Task<RfqPortalMasterData?> GetByIdAsync(int masterDataId)
    {
        return await _context.RfqPortalMasterDatas.FindAsync(masterDataId);
    }

    public async Task<IEnumerable<RfqPortalMasterData>> GetByTypeAsync(string type)
    {
        return await _context.RfqPortalMasterDatas
            .Where(m => m.Type == type)
            .ToListAsync();
    }

    public async Task<RfqPortalMasterData> AddAsync(RfqPortalMasterData masterData)
    {
        _context.RfqPortalMasterDatas.Add(masterData);
        await _context.SaveChangesAsync();
        return masterData;
    }

    public async Task<RfqPortalMasterData> UpdateAsync(RfqPortalMasterData masterData)
    {
        _context.RfqPortalMasterDatas.Update(masterData);
        await _context.SaveChangesAsync();
        return masterData;
    }

    public async Task DeleteAsync(int masterDataId)
    {
        var masterData = await _context.RfqPortalMasterDatas.FindAsync(masterDataId);
        if (masterData != null)
        {
            _context.RfqPortalMasterDatas.Remove(masterData);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int masterDataId)
    {
        return await _context.RfqPortalMasterDatas.AnyAsync(m => m.MasterDataId == masterDataId);
    }
}
