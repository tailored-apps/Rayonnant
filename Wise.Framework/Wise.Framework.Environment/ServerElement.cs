using System;
using System.Configuration;

namespace Wise.Framework.Environment
{
    public class ServerElement : ConfigurationElement
    {
        [ConfigurationProperty("EnvCode", DefaultValue = "PROD", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public String EnvCode
        {
            get
            {
                return (String)this["EnvCode"];
            }
            set
            {
                this["EnvCode"] = value;
            }

        }

        [ConfigurationProperty("Hostname", DefaultValue = "localhost", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public String Hostname
        {
            get
            {
                return (String)this["Hostname"];
            }
            set
            {
                this["Hostname"] = value;
            }

        }

        [ConfigurationProperty("Address", DefaultValue = "localhost", IsRequired = true)]
        public String Address
        {
            get
            {
                return (String)this["Address"];
            }
            set
            {
                this["Address"] = value;
            }

        }

    }
}
