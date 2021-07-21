using System;

namespace TailoredApps.Rayonnant.Presentation.Interface.ViewModel
{
    public interface IShellViewModel
    {
        /// <summary>
        ///     Title for shell window
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Uri to Icon image
        /// </summary>
        Uri Icon { get; set; }
    }
}