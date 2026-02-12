namespace Rayonnant.Module.MicroErp.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public string FullName => $"{FirstName} {LastName}".Trim();
}