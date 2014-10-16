using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.WindowsService;

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

        protected bool IsWorking
        {
            get { return this.isWorking; }
        }
        private void Initialize()
        {
            if (this.isInitialized)
                return;
            container.RegisterType<AbstractServiceBase, asd>(LifetimeScope.Singleton, "asd");
            this.isInitialized = true;
        }
        public void Dispose()
        {

        }

        private bool RunAsConsole(string[] args)
        {
            string[] console = new string[] { "C", "CONSOLE" };
            return args != null && args.Length > 0 && console.Contains(args[0].ToUpper());
        }

        private void Run(IEnumerable<AbstractServiceBase> services, TaskScheduler sched)
        {

            foreach (ServiceBase service in services)
                if (service != null && service is IStartable)
                    ((IStartable)service).Start(sched);

        }

        public void RunModule(string[] args)
        {
            this.Initialize();

            var runTask = Task.Factory.StartNew(() =>
            {
                var sched = TaskScheduler.Current;
                Console.WriteLine("Outer task executing.");

                var services = container.ResolveAll<AbstractServiceBase>();
                if (services != null)
                {
                    if (this.RunAsConsole(args))
                        this.Run(services, sched);
                    else
                        ServiceBase.Run(services.ToArray());
                }

                Console.WriteLine("Outer task end executing.");
            });

            runTask.Wait();
            Console.WriteLine("after Waiting.");
        }

        public bool StopPending { get; set; }
    }
}
