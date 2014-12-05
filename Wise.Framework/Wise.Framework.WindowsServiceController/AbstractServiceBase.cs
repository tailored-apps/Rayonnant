using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Wise.Framework.Interface.MultiThreading;
using Wise.Framework.Interface.WindowsServices;

namespace Wise.Framework.WindowsServiceController
{
    public abstract class AbstractServiceBase : ServiceBase, ITaskCreator, IWindowsService
    {
        public abstract Task CreateTask();

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            var task = CreateTask();
            task.Start(TaskScheduler.Current);
        }

        protected override void OnStop()
        {
            base.OnStop();
             
        }
    }

    public class asd : AbstractServiceBase
    {
        public override Task CreateTask()
        {
            var child = new Task(() =>
            {
                Console.WriteLine("Attached child starting.");
                Thread.Sleep(1000 * 5);
                Console.WriteLine("Attached child completing.");
            },
                TaskCreationOptions.AttachedToParent);
            return child;
        }

    }
}