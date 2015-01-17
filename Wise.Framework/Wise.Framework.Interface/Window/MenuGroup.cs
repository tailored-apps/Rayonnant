using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wise.Framework.Interface.Window
{
    public class MenuGroup
    {
        public string ElementName { get; set; }
        public string DataTemplateKey { get; set; }
        public ICommand Command { get; set; }
    }
}
