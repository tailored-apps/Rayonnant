using System;

namespace Wise.Framework.WindowsServiceController
{
    public interface IController : IDisposable
    {
        void RunModule(string[] args);
    }
}