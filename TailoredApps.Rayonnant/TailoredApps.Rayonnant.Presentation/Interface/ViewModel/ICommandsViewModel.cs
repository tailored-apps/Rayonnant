﻿using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TailoredApps.Rayonnant.Presentation.Interface.ViewModel
{
    /// <summary>
    ///     Interface used to present command in tool bars.
    /// </summary>
    public interface ICommandsViewModel
    {
        /// <summary>
        ///     Commands
        /// </summary>
        ObservableCollection<MenuItem> Commands { get; set; }
    }
}