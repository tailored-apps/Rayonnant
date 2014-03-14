using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.Environment;

namespace Wise.Framework.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        readonly EnvironmentSection config = (EnvironmentSection)ConfigurationManager.GetSection("environmentSection/environment");
        public IEnvironmentInfo GetEnvironmentInfo()
        {
            return new EnvironmentInfo()
            {
                SelfContained = config.SelfContained,
                Code = config.SelfContained ? System.Environment.MachineName : config.Server.EnvCode,
                HostName = config.SelfContained ? System.Environment.MachineName : config.Server.Hostname,
                Address = config.SelfContained ? "127.0.0.1" : config.Server.Address,
            };
        }
    }
}
