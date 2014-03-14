using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.DependencyInjection
{
    /// <summary>
    /// Static helper class container , which allow to resolve container object through application
    /// </summary>
    public static class Container
    {

        private static readonly object syncRoot = new object();
        private static volatile IContainer current;

        /// <summary>
        /// DI Container
        /// </summary>
        /// <exception cref="NullReferenceException">occurs when container is not setup-ed</exception>
        public static IContainer Current
        {
            get
            {
                if (current == null)
                {
                    lock (syncRoot)
                    {
                        if (current == null)
                        {
                            throw new NullReferenceException("current container is not initialized");
                        }
                    }
                }
                return current;
            }
            set
            {
                current = value;
            }
        }


        /// <summary>
        /// Returns state of container, true if is initialized
        /// </summary>
        public static bool IsInitialised
        {
            get { return current != null; }
        }

        /// <summary>
        /// ctor
        /// </summary>
        static Container()
        {

        }
    }
}
