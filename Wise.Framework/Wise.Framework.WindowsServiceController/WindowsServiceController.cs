using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using Common.Logging;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Wise.Framework.Interface.MultiThreading;

namespace Wise.Framework.WindowsServiceController
{
    public class WindowsServiceController : IWindowsServiceController
    {
        private readonly IContainer container;
        private readonly bool isWorking;
        private bool isInitialized;
        private readonly ILog logger;

        public WindowsServiceController(IContainer container, ILog logger)
        {
            this.container = container;
            this.logger = logger;
            isWorking = true;
            isInitialized = false;
        }

        protected bool IsWorking
        {
            get { return isWorking; }
        }


        public void Dispose()
        {
        }

        public void RunModule(string[] args)
        {
            Initialize();

            var runTask = Task.Factory.StartNew(() =>
            {
                var sched = TaskScheduler.Current;
                logger.InfoFormat("Task Sheduler Created: '{0}', maximum concurency level is : '{1}'", sched.Id, sched.MaximumConcurrencyLevel);

                var services = container.ResolveAll<AbstractServiceBase>();
                if (services != null)
                {
                    if (RunAsConsole(args))
                        Run(services, sched);
                    else
                        ServiceBase.Run(services.ToArray());

                    logger.InfoFormat("{0} service(s) started", services.Count());
                }
                else
                {
                    logger.InfoFormat("nothing to start");
                }

            });

            runTask.Wait();
            Console.WriteLine("after Waiting.");
        }

        private void Initialize()
        {

            logger.InfoFormat("Windows service is going to initialize");
            if (isInitialized)
                return;
            container.RegisterType<AbstractServiceBase, asd>(LifetimeScope.Singleton, "asd");
            isInitialized = true;
            logger.InfoFormat("Windows service is initialized");
        }

        private bool RunAsConsole(string[] args)
        {
            string[] console = { "C", "CONSOLE" };
            return args != null && args.Length > 0 && console.Contains(args[0].ToUpper());
        }

        private void Run(IEnumerable<AbstractServiceBase> services, TaskScheduler sched)
        {
            foreach (var task in services.OfType<ITaskCreator>())
            {
                task.CreateTask().Start(sched);
            }
        }
    }
}