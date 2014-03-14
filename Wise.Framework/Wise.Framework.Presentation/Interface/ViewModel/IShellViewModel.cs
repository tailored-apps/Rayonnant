using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Presentation.Interface.ViewModel
{
    public interface IShellViewModel
    {
        /// <summary>
        /// Title for shell window
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Uri to Icon image
        /// </summary>
        Uri Icon { get; set; }
    }
}
