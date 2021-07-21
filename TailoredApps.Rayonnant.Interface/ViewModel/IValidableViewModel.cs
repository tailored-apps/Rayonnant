using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailoredApps.Rayonnant.Interface.ViewModel
{
    public interface IValidableViewModel
    {
        bool Validate();
        IEnumerable<string> Errors { get; }
    }
}
