using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailoredApps.Rayonnant.Interface.WindowsServices
{
    public interface IWindowsService
    {
        bool AutoLog { get; set; }
        int ExitCode { get; set; }
        bool CanHandlePowerEvent { get; set; }
        bool CanHandleSessionChangeEvent { get; set; }
        bool CanPauseAndContinue { get; set; }
        bool CanShutdown { get; set; }
        bool CanStop { get; set; }
        EventLog EventLog { get;  }
        string ServiceName { get; set; }
       
        void Stop();

    }
}
