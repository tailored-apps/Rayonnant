using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;

namespace Wise.Framework.Interface.Window
{

        public interface IWindow
        {
            string Title { get; set; }

            double Height { get; set; }

            double Width { get; set; }
            bool Activate();
            void Close();
            void DragMove();
            void Hide();
            void Show();
            bool? ShowDialog();
            bool AllowsTransparency { set; get; }
            bool? DialogResult { set; get; }
            ImageSource Icon { set; get; }
            bool IsActive { get; }
            double Left { set; get; }
            WindowCollection OwnedWindows { get; }
            ResizeMode ResizeMode { set; get; }
            Rect RestoreBounds { get; }
            bool ShowActivated { set; get; }
            bool ShowInTaskbar { set; get; }
            SizeToContent SizeToContent { set; get; }
            TaskbarItemInfo TaskbarItemInfo { set; get; }
            double Top { set; get; }
            bool Topmost { set; get; }
            WindowStartupLocation WindowStartupLocation { set; get; }
            WindowState WindowState { set; get; }
            WindowStyle WindowStyle { set; get; }
            event EventHandler Activated;
            event EventHandler Closed;
            event CancelEventHandler Closing;
            event EventHandler ContentRendered;
            event EventHandler Deactivated;
            event EventHandler LocationChanged;
            event EventHandler SourceInitialized;
            event EventHandler StateChanged;

        
    }
}
