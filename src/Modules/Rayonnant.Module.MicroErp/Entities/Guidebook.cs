namespace Rayonnant.Module.MicroErp.Entities;

public class Guidebook
{
    public long Id { get; set; }
    public bool Riston { get; set; }
    public bool RistonTest { get; set; }
    public bool MosaicTest { get; set; }
    public bool MosaicPrinting { get; set; }
    public bool Etching { get; set; }
    public bool Cutting { get; set; }
    public bool Vscoring { get; set; }
    public bool Control { get; set; }
    public bool Packing { get; set; }
    public int FolderNumber { get; set; }
    public string ToPackingQty { get; set; } = string.Empty;
    public int Qty { get; set; }
    public bool Separation { get; set; }
    public bool Closed { get; set; }
    public bool Frezowanie { get; set; }
    public bool SeparationTester { get; set; }
    public bool SeparationSmt { get; set; }
    public bool Aoi { get; set; }
    public bool FrezowanieTester { get; set; }
    public string SeparationText { get; set; } = string.Empty;
    public int? OrderedQtyToMount { get; set; }
    public int? OrderedQtyToSell { get; set; }

    // Foreign key
    public long OrderId { get; set; }
    
    // Navigation property
    public Order Order { get; set; } = null!;
}