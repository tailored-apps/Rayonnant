using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.ViewModel
{
    public interface IValidableViewModel
    {
        bool Validate();
        IEnumerable<string> Errors { get; }
    }
}
