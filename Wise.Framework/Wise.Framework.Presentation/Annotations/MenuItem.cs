using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Presentation.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuItem : Attribute
    {
        public string Path { get; set; }
        public string DisplayName { get; set; }

        public MenuItem(string path,string displayName)
        {
            this.DisplayName = displayName;
            this.Path = path;
        }
    }
}
