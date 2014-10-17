using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationRequest
    {
        public string ViewModelFullName { get; set; }
        public Type ViewModelType { get; set; }
        public NavigationParameters UriQuery { get; set; }
    }
}
