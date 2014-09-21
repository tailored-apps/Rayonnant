using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.Modularity;

namespace Wise.Framework.Presentation.Modularity
{
    public class ModuleCatalog : Microsoft.Practices.Prism.Modularity.ModuleCatalog, IModuleCatalog, Microsoft.Practices.Prism.Modularity.IModuleCatalog
    {
        public IModuleCatalog AddModule(string moduleName, string moduleType, params string[] dependsOn)
        {
            base.AddModule(moduleName, moduleType, dependsOn);
            return this;
        }

        public IModuleCatalog AddModule(Type moduleType, params string[] dependsOn)
        {
            base.AddModule(moduleType, dependsOn);
            return this;
        }
    }
}
