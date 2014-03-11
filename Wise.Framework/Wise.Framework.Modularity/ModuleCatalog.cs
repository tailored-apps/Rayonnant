using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;

namespace Wise.Framework.Modularity
{
    public class ModuleCatalog : Microsoft.Practices.Prism.Modularity.ModuleCatalog , IModuleCatalog 
    {
        public void AddModule(ModuleInfo moduleInfo)
        {
            base.AddModule((ModuleInfo)moduleInfo);
        }


        public IEnumerable<ModuleInfo> Modules
        {
            get { throw new NotImplementedException(); }
        }

    }
}
