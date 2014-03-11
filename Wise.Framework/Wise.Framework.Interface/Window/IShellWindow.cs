using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.Window
{
    public interface IShellWindow
    {
        /// <summary>
        /// Window Width
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Window Height
        /// </summary>
        double Height { get; set; }


        /// <summary>
        /// Method responsible for showing window.
        /// </summary>
        void Show();
    }
}
