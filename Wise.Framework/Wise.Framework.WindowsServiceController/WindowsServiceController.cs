using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.WindowsServiceController
{
    public class WindowsServiceController : IController
    {
        private IContainer container;
        private bool isWorking;
        private bool isInitialized;

        public WindowsServiceController(IContainer container)
        {
            // TODO: Complete member initialization
            this.container = container;
            this.isWorking = true;
            this.isInitialized = false;
        }

        protected override bool IsWorking
        {
            get { return this.isWorking; }
        }
        private void Initialize()
        {
            if (this.isInitialized)
                return;
            //Hashtable properties = new Hashtable();
            //properties.Add("ModuleName", "#{moduleName}");
            //base.Register<ServiceBase, DefaultService>("serviceImpl", properties);
            this.isInitialized = true;
        }
        public void Dispose()
        {

        }
        private bool RunAsConsole(string[] args)
        {
            string[] console = new string[] { "c", "C", "console", "CONSOLE" };
            return args != null && args.Length > 0 && console.Contains(args[0]);
        }

        private void Run(IEnumerable<ServiceBase> services)
        {
            foreach (ServiceBase service in services)
                if (service != null && service is IEdfStartable)
                    ((IEdfStartable)service).Start();
            while (true)
            {
                if (base.StopPending)
                    break;

                Thread.Sleep(1000);
            }
        }

        public void RunModule(string[] args)
        {
            this.Initialize();
            IEnumerable<ServiceBase> services = container.ResolveAll<ServiceBase>();
            if (services != null)
            {


                if (this.RunAsConsole(args))
                    this.Run(services);
                else
                    ServiceBase.Run(services.ToArray());
            }
            this.isWorking = false;

        }
    }
}
