namespace Rayonnant.Module.MicroErp.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
    public string Person { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public string ElectronicInvoice { get; set; } = string.Empty;
    
    // Navigation properties
    public List<Pcb> Pcbs { get; set; } = [];
    public int? DefaultDeliveryTypeId { get; set; }
    public OrderDeliveryType? DefaultDeliveryType { get; set; }
}