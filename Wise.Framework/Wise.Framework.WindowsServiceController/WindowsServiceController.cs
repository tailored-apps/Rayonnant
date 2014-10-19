using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.WindowsService;

namespace Wise.Framework.WindowsServiceController
{
    public class WindowsServiceController : IController
    {
        private readonly IContainer container;
        private readonly bool isWorking;
        private bool isInitialized;

        public WindowsServiceController(IContainer container)
        {
            // TODO: Complete member initialization
            this.container = container;
            isWorking = true;
            isInitialized = false;
        }

        protected bool IsWorking
        {
            get { return isWorking; }
        }

        public bool StopPending { get; set; }

        public void Dispose()
        {
        }

        public void RunModule(string[] args)
        {
            Initialize();

            Task runTask = Task.Factory.StartNew(() =>
            {
                TaskScheduler sched = TaskScheduler.Current;
                Console.WriteLine("Outer task executing.");

                IEnumerable<AbstractServiceBase> services = container.ResolveAll<AbstractServiceBase>();
                if (services != null)
                {
                    if (RunAsConsole(args))
                        Run(services, sched);
                    else
                        ServiceBase.Run(services.ToArray());
                }

                Console.WriteLine("Outer task end executing.");
            });

            runTask.Wait();
            Console.WriteLine("after Waiting.");
        }

        private void Initialize()
        {
            if (isInitialized)
                return;
            container.RegisterType<AbstractServiceBase, asd>(LifetimeScope.Singleton, "asd");
            isInitialized = true;
        }

        private bool RunAsConsole(string[] args)
        {
            string[] console = {"C", "CONSOLE"};
            return args != null && args.Length > 0 && console.Contains(args[0].ToUpper());
        }

        private void Run(IEnumerable<AbstractServiceBase> services, TaskScheduler sched)
        {
            foreach (ServiceBase service in services)
                if (service != null && service is IStartable)
                    ((IStartable) service).Start(sched);
        }
    }
}