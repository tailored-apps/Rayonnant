using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using IModuleCatalog = Wise.Framework.Interface.Modularity.IModuleCatalog;

namespace Wise.Framework.Presentation.Modularity
{
    public class ModuleCatalog : Microsoft.Practices.Prism.Modularity.ModuleCatalog, IModuleCatalog
    {
        public IModuleCatalog AddModule(string moduleName, string moduleType,Uri moduleUri, params string[] dependsOn)
        {
            var moduleInfo = new ModuleInfo()
            {
                ModuleName = moduleName,
                ModuleType = moduleType,
                DependsOn = new Collection<string>(dependsOn),
                Ref = moduleUri.AbsoluteUri
            };
            
            base.AddModule(moduleInfo);
            return this;
        }

        public IModuleCatalog AddModule(Type moduleType, params string[] dependsOn)
        {
            base.AddModule(moduleType, dependsOn);
            return this;
        }
    }
}