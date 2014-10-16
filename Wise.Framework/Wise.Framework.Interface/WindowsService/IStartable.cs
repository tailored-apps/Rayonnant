using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.WindowsService
{
    public interface IStartable
    {
        void Start();
        void Start(TaskScheduler scheduler);
    }
}
