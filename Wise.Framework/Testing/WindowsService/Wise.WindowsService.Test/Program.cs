using Wise.Framework.WindowsServiceController;

namespace Wise.WindowsService.Test
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main(params string[] args)
        {
            using (var wsc = new WindowsServiceRunner())
            {

                wsc.RegisterService<asd>();
                
                wsc.RunRegisteredServices(args);
            };
        }
    }
}