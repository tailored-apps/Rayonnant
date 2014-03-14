using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.ExceptionHandling
{
    /// <summary>
    /// Interface responsible for showing exception message box.
    /// </summary>
    public interface IExceptionService
    { 
        /// <summary>
        /// Interface method for showing and deciding application next steps when some exception is crashing application
        /// </summary>
        /// <param name="exception">exception to present</param>
        /// <param name="options">window button options</param>
        bool? ShowDialog(Exception exception, ExceptionOptions options);

        /// <summary>
        /// Same like <see cref="ShowDialog(System.Exception,ExceptionOptions)"/> but contains 
        /// additional message for presentation in window.
        /// </summary>
        /// <param name="exception">exception to show</param>
        /// <param name="options">window button options</param>
        /// <param name="message">message to present</param>
        bool? ShowDialog(Exception exception, ExceptionOptions options, string message);
    }
}
