using System;

namespace Wise.Framework.Interface.Modularity
{
    public interface IModuleCatalog
    {
        IModuleCatalog AddModule(string moduleName, string moduleType, params string[] dependsOn);
        IModuleCatalog AddModule(Type moduleType, params string[] dependsOn);
    }
}