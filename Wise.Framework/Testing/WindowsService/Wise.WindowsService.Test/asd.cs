using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wise.Framework.WindowsServiceController;

namespace Wise.WindowsService.Test
{
    public class asd : AbstractServiceBase
    {
        public override Task CreateTask(CancellationTokenSource cancelationToken)
        {
            var child = new Task(() =>
            {
                while (true)
                {
                    if (cancelationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    
                    Console.WriteLine("Attached child starting.");
                    Thread.Sleep(1000 * 5);
                    Console.WriteLine("Attached child completing.");
                }
            },
                TaskCreationOptions.AttachedToParent);
            return child;
        }

        public asd(CancellationTokenSource cancellationTokenSource) : base(cancellationTokenSource)
        {

        }
    }
}
