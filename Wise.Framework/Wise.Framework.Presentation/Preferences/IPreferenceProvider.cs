using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Presentation.Preferences
{
    public interface IPreferenceProvider
    {
        string HomeView { get; set; }

        void Save();
    }
}
