namespace Rayonnant.Module.MicroErp.Entities;

public class Material
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CopperThicknessInUm { get; set; } = string.Empty;
    public double MaterialThicknessInMm { get; set; }
    public bool Obsolete { get; set; }
    public bool TwoSided { get; set; }
    
    public string FullMaterialName => $"{Name} | {MaterialThicknessInMm:F} | {CopperThicknessInUm}";
}