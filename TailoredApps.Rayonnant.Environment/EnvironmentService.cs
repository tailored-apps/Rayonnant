using System.Configuration;
using TailoredApps.Rayonnant.Interface.Environment;

namespace TailoredApps.Rayonnant.Environment
{
    public class EnvironmentService : IEnvironmentService
    {

        public IEnvironmentInfo GetEnvironmentInfo()
        {//TODO FIX THAT CODE
            return new EnvironmentInfo();
            var config = (EnvironmentSection)ConfigurationManager.GetSection("environmentSection/environment");
            var localhost = "127.0.0.1";
            if (config == null)
            {
                return new EnvironmentInfo
                {
                    SelfContained = true,
                    Code = System.Environment.MachineName ,
                    HostName = System.Environment.MachineName,
                    Address = localhost ,
                };
            }
            return new EnvironmentInfo
            {
                SelfContained = config.SelfContained,
                Code = config.SelfContained ? System.Environment.MachineName : config.Server.EnvCode,
                HostName = config.SelfContained ? System.Environment.MachineName : config.Server.Hostname,
                Address = config.SelfContained ? localhost : config.Server.Address,
            };
        }
    }
}