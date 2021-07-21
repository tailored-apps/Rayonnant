namespace TailoredApps.Rayonnant.Interface.Environment
{
    public interface IEnvironmentInfo
    {
        bool SelfContained { get; set; }
        string Code { get; set; }
        string Address { get; set; }
        string HostName { get; set; }
    }
}