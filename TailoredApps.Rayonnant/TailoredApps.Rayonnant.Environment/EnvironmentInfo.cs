using TailoredApps.Rayonnant.Interface.Environment;

namespace TailoredApps.Rayonnant.Environment
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        public bool SelfContained { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string HostName { get; set; }
    }
}