namespace Wise.Framework.Interface.ExceptionHandling
{
    /// <summary>
    /// Describes available buttons for exception window.
    /// </summary>
    public enum ExceptionOptions
    {
        /// <summary>
        /// The Exit Button, also only close application
        /// </summary>
        Exit,
        /// <summary>
        /// The Continue Button, also after crashing just continue execution with breaking current operations
        /// </summary>
        Continue,
        /// <summary>
        /// User can choose exit or continue after crashing
        /// </summary>
        ExitOrContinue
    }
}
