using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Services;

public class CustomerService
{
    private readonly MicroErpDbContext _context;

    public CustomerService(MicroErpDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .Include(c => c.DefaultDeliveryType)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.DefaultDeliveryType)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        return await _context.Customers
            .Include(c => c.DefaultDeliveryType)
            .Where(c => c.Name.Contains(searchTerm) || c.Person.Contains(searchTerm) || c.City.Contains(searchTerm))
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}