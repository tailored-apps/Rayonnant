using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Services;

public class GuidebookService
{
    private readonly MicroErpDbContext _context;

    public GuidebookService(MicroErpDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guidebook>> GetAllAsync()
    {
        return await _context.Guidebooks
            .Include(g => g.Order)
                .ThenInclude(o => o.Pcb)
                    .ThenInclude(p => p.Customer)
            .OrderByDescending(g => g.FolderNumber)
            .ToListAsync();
    }

    public async Task<List<Guidebook>> GetActiveGuidebooksAsync()
    {
        return await _context.Guidebooks
            .Include(g => g.Order)
                .ThenInclude(o => o.Pcb)
                    .ThenInclude(p => p.Customer)
            .Where(g => !g.Closed)
            .OrderBy(g => g.FolderNumber)
            .ToListAsync();
    }

    public async Task<List<Guidebook>> GetCompletedGuidebooksAsync()
    {
        return await _context.Guidebooks
            .Include(g => g.Order)
                .ThenInclude(o => o.Pcb)
                    .ThenInclude(p => p.Customer)
            .Where(g => g.Closed)
            .OrderByDescending(g => g.FolderNumber)
            .ToListAsync();
    }

    public async Task<Guidebook?> GetByIdAsync(long id)
    {
        return await _context.Guidebooks
            .Include(g => g.Order)
                .ThenInclude(o => o.Pcb)
                    .ThenInclude(p => p.Customer)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Guidebook>> GetByOrderAsync(long orderId)
    {
        return await _context.Guidebooks
            .Include(g => g.Order)
            .Where(g => g.OrderId == orderId)
            .OrderBy(g => g.FolderNumber)
            .ToListAsync();
    }

    public async Task<Guidebook> CreateAsync(Guidebook guidebook)
    {
        // Generate next folder number
        var lastFolderNumber = await _context.Guidebooks
            .MaxAsync(g => (int?)g.FolderNumber) ?? 2025000;
        
        guidebook.FolderNumber = lastFolderNumber + 1;
        
        _context.Guidebooks.Add(guidebook);
        await _context.SaveChangesAsync();
        return guidebook;
    }

    public async Task<Guidebook> UpdateAsync(Guidebook guidebook)
    {
        _context.Guidebooks.Update(guidebook);
        await _context.SaveChangesAsync();
        return guidebook;
    }

    public async Task<Guidebook> ToggleStepAsync(long id, string stepName)
    {
        var guidebook = await GetByIdAsync(id);
        if (guidebook == null) throw new ArgumentException("Guidebook not found");

        var property = guidebook.GetType().GetProperty(stepName);
        if (property?.PropertyType == typeof(bool))
        {
            var currentValue = (bool)property.GetValue(guidebook)!;
            property.SetValue(guidebook, !currentValue);
            await UpdateAsync(guidebook);
        }

        return guidebook;
    }

    public async Task DeleteAsync(long id)
    {
        var guidebook = await _context.Guidebooks.FindAsync(id);
        if (guidebook != null)
        {
            _context.Guidebooks.Remove(guidebook);
            await _context.SaveChangesAsync();
        }
    }
}