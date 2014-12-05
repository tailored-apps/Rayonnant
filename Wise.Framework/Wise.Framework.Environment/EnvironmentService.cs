using System.Configuration;
using Wise.Framework.Interface.Environment;

namespace Wise.Framework.Environment
{
    public class EnvironmentService : IEnvironmentService
    {

        public IEnvironmentInfo GetEnvironmentInfo()
        {
            var config = (EnvironmentSection)ConfigurationManager.GetSection("environmentSection/environment");
            if (config == null)
            {
                return new EnvironmentInfo
                {
                    SelfContained = true,
                    Code = System.Environment.MachineName ,
                    HostName = System.Environment.MachineName,
                    Address = "127.0.0.1" ,
                };
            }
            return new EnvironmentInfo
            {
                SelfContained = config.SelfContained,
                Code = config.SelfContained ? System.Environment.MachineName : config.Server.EnvCode,
                HostName = config.SelfContained ? System.Environment.MachineName : config.Server.Hostname,
                Address = config.SelfContained ? "127.0.0.1" : config.Server.Address,
            };
        }
    }
}