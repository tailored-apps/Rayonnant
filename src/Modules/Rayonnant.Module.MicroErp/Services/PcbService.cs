using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Services;

public class PcbService
{
    private readonly MicroErpDbContext _context;

    public PcbService(MicroErpDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pcb>> GetAllAsync()
    {
        return await _context.Pcbs
            .Include(p => p.Customer)
            .Include(p => p.Material)
            .Include(p => p.DefaultTopSolder)
            .Include(p => p.DefaultBottomSolder)
            .Include(p => p.DefaultTopOverlay)
            .Include(p => p.DefaultBottomOverlay)
            .Include(p => p.Cover)
            .Where(p => !p.Obsolete)
            .OrderBy(p => p.Customer.Name)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Pcb?> GetByIdAsync(long id)
    {
        return await _context.Pcbs
            .Include(p => p.Customer)
            .Include(p => p.Material)
            .Include(p => p.DefaultTopSolder)
            .Include(p => p.DefaultBottomSolder)
            .Include(p => p.DefaultTopOverlay)
            .Include(p => p.DefaultBottomOverlay)
            .Include(p => p.Cover)
            .Include(p => p.DefaultThtMountType)
            .Include(p => p.DefaultThtMountTechnology)
            .Include(p => p.DefaultSmtMountType)
            .Include(p => p.DefaultSmtMountTechnology)
            .Include(p => p.ComponentsDeliveryType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pcb>> GetByCustomerAsync(int customerId)
    {
        return await _context.Pcbs
            .Include(p => p.Customer)
            .Include(p => p.Material)
            .Where(p => p.CustomerId == customerId && !p.Obsolete)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Pcb> CreateAsync(Pcb pcb)
    {
        _context.Pcbs.Add(pcb);
        await _context.SaveChangesAsync();
        return pcb;
    }

    public async Task<Pcb> UpdateAsync(Pcb pcb)
    {
        _context.Pcbs.Update(pcb);
        await _context.SaveChangesAsync();
        return pcb;
    }

    public async Task DeleteAsync(long id)
    {
        var pcb = await _context.Pcbs.FindAsync(id);
        if (pcb != null)
        {
            pcb.Obsolete = true; // Soft delete
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Pcb>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        return await _context.Pcbs
            .Include(p => p.Customer)
            .Include(p => p.Material)
            .Where(p => !p.Obsolete && 
                       (p.Name.Contains(searchTerm) || 
                        p.Customer.Name.Contains(searchTerm) ||
                        p.Comments.Contains(searchTerm)))
            .OrderBy(p => p.Customer.Name)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }
}