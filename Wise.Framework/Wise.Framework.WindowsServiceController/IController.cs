using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wise.Framework.WindowsServiceController
{
    public interface IController : IDisposable
    {

        void RunModule(string[] args);
    }
}
