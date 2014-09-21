using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.Modularity
{
   public interface IModuleCatalog
   {
       IModuleCatalog AddModule(string moduleName, string moduleType, params string[] dependsOn);
       IModuleCatalog AddModule(Type moduleType, params string[] dependsOn);
   }
}
