using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.WindowsService;

namespace Wise.Framework.WindowsServiceController
{
    public abstract class AbstractServiceBase : ServiceBase, IStartable
    {
        public void Start()
        {
            //throw new NotImplementedException();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }

    public class asd : AbstractServiceBase
    {

    }
}
