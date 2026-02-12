namespace Rayonnant.Module.MicroErp.Entities;

public class Order
{
    public long Id { get; set; }
    public string OrdererName { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Today;
    public DateTime OrderExpirationDate { get; set; } = DateTime.Today.AddDays(14);
    public bool HolePlatting { get; set; }
    public bool SmtTemplate { get; set; }
    public string SmtTemplateThickness { get; set; } = string.Empty;
    public bool Smt { get; set; }
    public bool Tht { get; set; }
    public int OrderedQty { get; set; }
    public int? OrderedQtyToMount { get; set; }
    public int? OrderedQtyToSell { get; set; }
    public bool GuideBookDrawn { get; set; }
    public int? DoneQty { get; set; }
    public int? WasteQty { get; set; }
    public bool OrderFinished { get; set; }
    public bool Collected { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public string Comments { get; set; } = string.Empty;
    public string OfferNo { get; set; } = string.Empty;
    public bool Express { get; set; }
    public bool ElectronicPartsCollected { get; set; }
    public string ElectronicInvoice { get; set; } = string.Empty;
    public DateTime? PartsDeliveryDate { get; set; }
    public DateTime? PartsDeliveryCompletionDate { get; set; }

    // Foreign keys
    public long PcbId { get; set; }
    public int? DocumentationId { get; set; }
    public int? SolderLayerTopId { get; set; }
    public int? SolderLayerBottomId { get; set; }
    public int? OverlayLayerTopId { get; set; }
    public int? OverlayLayerBottomId { get; set; }
    public int? CoverTypeId { get; set; }
    public int? OrderTypeId { get; set; }
    public int? EmployeeId { get; set; }
    public int? SmtMountTechnologyId { get; set; }
    public int? ThtMountTechnologyId { get; set; }
    public int? SmtMountTypeId { get; set; }
    public int? ThtMountTypeId { get; set; }
    public int? DeliveryTypeId { get; set; }
    public int? ComponentsDeliveryTypeId { get; set; }
    public int? MaterialId { get; set; }

    // Navigation properties
    public Pcb Pcb { get; set; } = null!;
    public DocumentationType? Documentation { get; set; }
    public SolderColor? SolderLayerTop { get; set; }
    public SolderColor? SolderLayerBottom { get; set; }
    public OverlayColor? OverlayLayerTop { get; set; }
    public OverlayColor? OverlayLayerBottom { get; set; }
    public CoverType? CoverType { get; set; }
    public OrderType? OrderType { get; set; }
    public Employee? Employee { get; set; }
    public AssemblyTechnologyType? SmtMountTechnology { get; set; }
    public AssemblyTechnologyType? ThtMountTechnology { get; set; }
    public AssemblyType? SmtMountType { get; set; }
    public AssemblyType? ThtMountType { get; set; }
    public OrderDeliveryType? DeliveryType { get; set; }
    public ComponentsDeliveryType? ComponentsDeliveryType { get; set; }
    public Material? Material { get; set; }
    
    public List<Guidebook> Guidebooks { get; set; } = [];
}