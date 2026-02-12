using Microsoft.EntityFrameworkCore;
using Rayonnant.Module.MicroErp.Data;
using Rayonnant.Module.MicroErp.Entities;

namespace Rayonnant.Module.MicroErp.Services;

public class ConfigurationService
{
    private readonly MicroErpDbContext _context;

    public ConfigurationService(MicroErpDbContext context)
    {
        _context = context;
    }

    // Solder Colors
    public async Task<List<SolderColor>> GetSolderColorsAsync() =>
        await _context.SolderColors.OrderBy(x => x.Name).ToListAsync();

    public async Task<SolderColor> SaveSolderColorAsync(SolderColor solderColor)
    {
        if (solderColor.Id == 0)
            _context.SolderColors.Add(solderColor);
        else
            _context.SolderColors.Update(solderColor);
        
        await _context.SaveChangesAsync();
        return solderColor;
    }

    public async Task DeleteSolderColorAsync(int id)
    {
        var item = await _context.SolderColors.FindAsync(id);
        if (item != null)
        {
            _context.SolderColors.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Overlay Colors
    public async Task<List<OverlayColor>> GetOverlayColorsAsync() =>
        await _context.OverlayColors.OrderBy(x => x.Name).ToListAsync();

    public async Task<OverlayColor> SaveOverlayColorAsync(OverlayColor overlayColor)
    {
        if (overlayColor.Id == 0)
            _context.OverlayColors.Add(overlayColor);
        else
            _context.OverlayColors.Update(overlayColor);
        
        await _context.SaveChangesAsync();
        return overlayColor;
    }

    public async Task DeleteOverlayColorAsync(int id)
    {
        var item = await _context.OverlayColors.FindAsync(id);
        if (item != null)
        {
            _context.OverlayColors.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Materials
    public async Task<List<Material>> GetMaterialsAsync() =>
        await _context.Materials.Where(x => !x.Obsolete).OrderBy(x => x.Name).ToListAsync();

    public async Task<Material> SaveMaterialAsync(Material material)
    {
        if (material.Id == 0)
            _context.Materials.Add(material);
        else
            _context.Materials.Update(material);
        
        await _context.SaveChangesAsync();
        return material;
    }

    public async Task DeleteMaterialAsync(int id)
    {
        var item = await _context.Materials.FindAsync(id);
        if (item != null)
        {
            item.Obsolete = true; // Soft delete
            await _context.SaveChangesAsync();
        }
    }

    // Cover Types
    public async Task<List<CoverType>> GetCoverTypesAsync() =>
        await _context.CoverTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<CoverType> SaveCoverTypeAsync(CoverType coverType)
    {
        if (coverType.Id == 0)
            _context.CoverTypes.Add(coverType);
        else
            _context.CoverTypes.Update(coverType);
        
        await _context.SaveChangesAsync();
        return coverType;
    }

    public async Task DeleteCoverTypeAsync(int id)
    {
        var item = await _context.CoverTypes.FindAsync(id);
        if (item != null)
        {
            _context.CoverTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Assembly Types
    public async Task<List<AssemblyType>> GetAssemblyTypesAsync() =>
        await _context.AssemblyTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<AssemblyType> SaveAssemblyTypeAsync(AssemblyType assemblyType)
    {
        if (assemblyType.Id == 0)
            _context.AssemblyTypes.Add(assemblyType);
        else
            _context.AssemblyTypes.Update(assemblyType);
        
        await _context.SaveChangesAsync();
        return assemblyType;
    }

    public async Task DeleteAssemblyTypeAsync(int id)
    {
        var item = await _context.AssemblyTypes.FindAsync(id);
        if (item != null)
        {
            _context.AssemblyTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Assembly Technology Types
    public async Task<List<AssemblyTechnologyType>> GetAssemblyTechnologyTypesAsync() =>
        await _context.AssemblyTechnologyTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<AssemblyTechnologyType> SaveAssemblyTechnologyTypeAsync(AssemblyTechnologyType technologyType)
    {
        if (technologyType.Id == 0)
            _context.AssemblyTechnologyTypes.Add(technologyType);
        else
            _context.AssemblyTechnologyTypes.Update(technologyType);
        
        await _context.SaveChangesAsync();
        return technologyType;
    }

    public async Task DeleteAssemblyTechnologyTypeAsync(int id)
    {
        var item = await _context.AssemblyTechnologyTypes.FindAsync(id);
        if (item != null)
        {
            _context.AssemblyTechnologyTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Order Types
    public async Task<List<OrderType>> GetOrderTypesAsync() =>
        await _context.OrderTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<OrderType> SaveOrderTypeAsync(OrderType orderType)
    {
        if (orderType.Id == 0)
            _context.OrderTypes.Add(orderType);
        else
            _context.OrderTypes.Update(orderType);
        
        await _context.SaveChangesAsync();
        return orderType;
    }

    public async Task DeleteOrderTypeAsync(int id)
    {
        var item = await _context.OrderTypes.FindAsync(id);
        if (item != null)
        {
            _context.OrderTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Order Delivery Types
    public async Task<List<OrderDeliveryType>> GetOrderDeliveryTypesAsync() =>
        await _context.OrderDeliveryTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<OrderDeliveryType> SaveOrderDeliveryTypeAsync(OrderDeliveryType deliveryType)
    {
        if (deliveryType.Id == 0)
            _context.OrderDeliveryTypes.Add(deliveryType);
        else
            _context.OrderDeliveryTypes.Update(deliveryType);
        
        await _context.SaveChangesAsync();
        return deliveryType;
    }

    public async Task DeleteOrderDeliveryTypeAsync(int id)
    {
        var item = await _context.OrderDeliveryTypes.FindAsync(id);
        if (item != null)
        {
            _context.OrderDeliveryTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Components Delivery Types
    public async Task<List<ComponentsDeliveryType>> GetComponentsDeliveryTypesAsync() =>
        await _context.ComponentsDeliveryTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<ComponentsDeliveryType> SaveComponentsDeliveryTypeAsync(ComponentsDeliveryType deliveryType)
    {
        if (deliveryType.Id == 0)
            _context.ComponentsDeliveryTypes.Add(deliveryType);
        else
            _context.ComponentsDeliveryTypes.Update(deliveryType);
        
        await _context.SaveChangesAsync();
        return deliveryType;
    }

    public async Task DeleteComponentsDeliveryTypeAsync(int id)
    {
        var item = await _context.ComponentsDeliveryTypes.FindAsync(id);
        if (item != null)
        {
            _context.ComponentsDeliveryTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Documentation Types
    public async Task<List<DocumentationType>> GetDocumentationTypesAsync() =>
        await _context.DocumentationTypes.OrderBy(x => x.Name).ToListAsync();

    public async Task<DocumentationType> SaveDocumentationTypeAsync(DocumentationType documentationType)
    {
        if (documentationType.Id == 0)
            _context.DocumentationTypes.Add(documentationType);
        else
            _context.DocumentationTypes.Update(documentationType);
        
        await _context.SaveChangesAsync();
        return documentationType;
    }

    public async Task DeleteDocumentationTypeAsync(int id)
    {
        var item = await _context.DocumentationTypes.FindAsync(id);
        if (item != null)
        {
            _context.DocumentationTypes.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    // Employees
    public async Task<List<Employee>> GetEmployeesAsync() =>
        await _context.Employees.Where(x => x.IsActive).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync();
}