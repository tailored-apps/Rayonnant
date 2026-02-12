using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Services;

public class OrderService
{
    private readonly MicroErpDbContext _context;

    public OrderService(MicroErpDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Pcb)
                .ThenInclude(p => p.Customer)
            .Include(o => o.Employee)
            .Include(o => o.OrderType)
            .Include(o => o.SolderLayerTop)
            .Include(o => o.SolderLayerBottom)
            .Include(o => o.OverlayLayerTop)
            .Include(o => o.OverlayLayerBottom)
            .Include(o => o.CoverType)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await _context.Orders
            .Include(o => o.Pcb)
                .ThenInclude(p => p.Customer)
            .Include(o => o.Employee)
            .Include(o => o.OrderType)
            .Include(o => o.SolderLayerTop)
            .Include(o => o.SolderLayerBottom)
            .Include(o => o.OverlayLayerTop)
            .Include(o => o.OverlayLayerBottom)
            .Include(o => o.CoverType)
            .Include(o => o.Material)
            .Include(o => o.Documentation)
            .Include(o => o.DeliveryType)
            .Include(o => o.ComponentsDeliveryType)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetActiveOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Pcb)
                .ThenInclude(p => p.Customer)
            .Include(o => o.Employee)
            .Where(o => !o.OrderFinished)
            .OrderBy(o => o.OrderExpirationDate)
            .ToListAsync();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(long id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Order>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        return await _context.Orders
            .Include(o => o.Pcb)
                .ThenInclude(p => p.Customer)
            .Include(o => o.Employee)
            .Where(o => o.CustomerName.Contains(searchTerm) || 
                       o.Pcb.Name.Contains(searchTerm) ||
                       o.OrdererName.Contains(searchTerm))
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
}