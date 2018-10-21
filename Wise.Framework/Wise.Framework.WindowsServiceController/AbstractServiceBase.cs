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
        private readonly CancellationTokenSource cancellationTokenSource;
        public AbstractServiceBase(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public abstract Task CreateTask(CancellationTokenSource cancelationToken);


        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            var task = CreateTask(cancellationTokenSource);
            task.Start(TaskScheduler.Current);
            Task = task;
        }
        public Task Task { get; private set; }
        protected override void OnStop()
        {
            cancellationTokenSource.Cancel();
            base.OnStop();

        }
    }

}