namespace Rayonnant.Module.MicroErp.Entities;

public class Pcb
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double PrototypePrice { get; set; }
    public double ProductionPrice { get; set; }
    public bool HolePlatting { get; set; }
    public bool Milling { get; set; }
    public bool CuttingOnEdge { get; set; }
    public bool Vscoring { get; set; }
    public string Comments { get; set; } = string.Empty;
    public string SeparationText { get; set; } = string.Empty;
    public string CollectionSet { get; set; } = string.Empty;
    public bool Obsolete { get; set; }
    public int? EnvelopeNumber { get; set; }

    // Foreign keys
    public int CustomerId { get; set; }
    public int? MaterialId { get; set; }
    public int? DefaultTopSolderId { get; set; }
    public int? DefaultBottomSolderId { get; set; }
    public int? DefaultTopOverlayId { get; set; }
    public int? DefaultBottomOverlayId { get; set; }
    public int? CoverId { get; set; }
    public int? DefaultThtMountTypeId { get; set; }
    public int? DefaultThtMountTechnologyId { get; set; }
    public int? DefaultSmtMountTypeId { get; set; }
    public int? DefaultSmtMountTechnologyId { get; set; }
    public int? ComponentsDeliveryTypeId { get; set; }

    // Additional checkboxes
    public bool FrezowanieTester { get; set; }
    public bool Aoi { get; set; }
    public bool SeparationTester { get; set; }
    public bool SeparationSmt { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public Material? Material { get; set; }
    public SolderColor? DefaultTopSolder { get; set; }
    public SolderColor? DefaultBottomSolder { get; set; }
    public OverlayColor? DefaultTopOverlay { get; set; }
    public OverlayColor? DefaultBottomOverlay { get; set; }
    public CoverType? Cover { get; set; }
    public AssemblyType? DefaultThtMountType { get; set; }
    public AssemblyTechnologyType? DefaultThtMountTechnology { get; set; }
    public AssemblyType? DefaultSmtMountType { get; set; }
    public AssemblyTechnologyType? DefaultSmtMountTechnology { get; set; }
    public ComponentsDeliveryType? ComponentsDeliveryType { get; set; }
    
    public List<Order> Orders { get; set; } = [];
}