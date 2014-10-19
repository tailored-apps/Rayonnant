namespace Wise.Framework.Interface.Window
{
    public interface IShellWindow : IWindow
    {
        /// <summary>
        ///     Window Width
        /// </summary>
        double Width { get; set; }

        /// <summary>
        ///     Window Height
        /// </summary>
        double Height { get; set; }


        /// <summary>
        ///     Method responsible for showing window.
        /// </summary>
        void Show();
    }
}