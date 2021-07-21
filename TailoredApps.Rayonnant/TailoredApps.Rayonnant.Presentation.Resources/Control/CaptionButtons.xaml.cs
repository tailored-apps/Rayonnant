using System.Windows;
using TailoredApps.Rayonnant.Interface.Window;
using TailoredApps.Rayonnant.Presentation.ViewModel;
using TailoredApps.Rayonnant.Presentation.Window;

namespace TailoredApps.Rayonnant.Presentation.Resources.Control
{
    /// <summary>
    ///     Interaction logic for CaptionButtons.xaml
    /// </summary>
    public partial class CaptionButtons
    {
        /// <summary>
        ///     Enum of the types of caption buttons
        /// </summary>
        public enum CaptionType
        {
            /// <summary>
            /// All with dock button
            /// </summary>
            FullDock,
            /// <summary>
            ///     All the buttons
            /// </summary>
            Full,

            /// <summary>
            ///     Only the close button
            /// </summary>
            Close,

            /// <summary>
            ///     Reduce and close buttons
            /// </summary>
            ReduceClose
        }

        /// <summary>
        ///     The dependency property for the Margin between the buttons.
        /// </summary>
        public static DependencyProperty MarginButtonProperty = DependencyProperty.Register(
            "MarginButton",
            typeof (Thickness),
            typeof (ShellWindow));

        /// <summary>
        ///     The dependency property for the Margin between the buttons.
        /// </summary>
        public static DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type",
            typeof (CaptionType),
            typeof (ShellWindow),
            new PropertyMetadata(CaptionType.Full));

        /// <summary>
        ///     The parent Window of the control.
        /// </summary>
        private System.Windows.Window _parent;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CaptionButtons" /> class.
        /// </summary>
        public CaptionButtons()
        {
            InitializeComponent();
            Loaded += CaptionButtonsLoaded;
        }

        /// <summary>
        ///     Gets the top parent (Window).
        /// </summary>
        /// <returns>The parent Window.</returns>
        protected System.Windows.Window ParentWindow
        {
            get { return System.Windows.Window.GetWindow(this); }
        }

        /// <summary>
        ///     Gets or sets the margin button.
        /// </summary>
        /// <value>The margin button.</value>
        public Thickness MarginButton
        {
            get { return (Thickness) GetValue(MarginButtonProperty); }
            set { base.SetValue(MarginButtonProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the visibility of the buttons.
        /// </summary>
        /// <value>The visible buttons.</value>
        public CaptionType Type
        {
            get { return (CaptionType) GetValue(TypeProperty); }
            set { base.SetValue(TypeProperty, value); }
        }

        /// <summary>
        ///     Event when the control is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void CaptionButtonsLoaded(object sender, RoutedEventArgs e)
        {
            _parent = ParentWindow;
        }

        /// <summary>
        ///     Action on the button to close the window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            _parent.Close();
        }

        /// <summary>
        ///     Changes the view of the window (maximized or normal).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void RestoreButtonClick(object sender, RoutedEventArgs e)
        {
            _parent.WindowState = _parent.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        /// <summary>
        ///     Minimizes the Window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            _parent.WindowState = WindowState.Minimized;
        }
        /// <summary>
        ///     Minimizes the Window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void DockButtonClick(object sender, RoutedEventArgs e)
        {
            var prismViewModel = _parent as IModalWindow;
           
            if (prismViewModel != null)
                prismViewModel.Dock();
        }
    }
}