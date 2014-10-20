using Wise.Framework.Interface.Environment;

namespace Wise.Framework.Environment
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        public bool SelfContained { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string HostName { get; set; }
    }
}