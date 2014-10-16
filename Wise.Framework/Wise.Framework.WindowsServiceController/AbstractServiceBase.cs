using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wise.Framework.Interface.WindowsService;

namespace Wise.Framework.WindowsServiceController
{
    public abstract class AbstractServiceBase : ServiceBase, IStartable
    {
        public abstract void Start();
        public abstract void Start(TaskScheduler scheduler);

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            Start(TaskScheduler.Current);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }

    public class asd : AbstractServiceBase
    {

        public override void Start(TaskScheduler scheduler)
        {
            var child = new Task(() =>
            {

                Console.WriteLine("Attached child starting.");
                Thread.Sleep(1000 * 5);
                Console.WriteLine("Attached child completing.");

            },
            TaskCreationOptions.AttachedToParent);

            child.Start(scheduler);
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
