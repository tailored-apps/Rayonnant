using System;

namespace Wise.Framework.WindowsServiceController
{
    public interface IWindowsServiceController : IDisposable
    {
        void RunModule(string[] args);
    }
}