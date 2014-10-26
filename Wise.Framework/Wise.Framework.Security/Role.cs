using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wise.Framework.Interface.Security;

namespace Wise.Framework.Security
{
    public class Role : IRole
    {
        private Role(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }

        public static Role Create(string name)
        {
            return new Role(name);
        }
    }



}
