using System;
using System.Configuration;

namespace Wise.Framework.Environment
{
    public class EnvironmentSection : ConfigurationSection
    {
        [ConfigurationProperty("SelfContained", DefaultValue = "true", IsRequired = true)]
        public Boolean SelfContained
        {
            get { return (Boolean) this["SelfContained"]; }
            set { this["SelfContained"] = value; }
        }

        [ConfigurationProperty("Server", IsRequired = false)]
        public ServerElement Server
        {
            get { return (ServerElement) this["Server"]; }
            set { this["Server"] = value; }
        }
    }
}