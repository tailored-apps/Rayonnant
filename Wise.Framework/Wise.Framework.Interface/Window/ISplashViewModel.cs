using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wise.Framework.Interface.Window
{
   public  interface ISplashViewModel
   {
       /// <summary>
       /// Messages
       /// </summary>
       ObservableCollection<string> Messages { get; set; }
       /// <summary>
       /// Last Message
       /// </summary>
       string CurrentMessage { set; get; }
       /// <summary>
       /// Application name
       /// </summary>
       string ApplicationName { get; set; }
       /// <summary>
       /// Environment  name
       /// </summary>
       string EnviormentName { get; set; }
       /// <summary>
       /// Product name
       /// </summary>
       string ProductName { get; set; }
       /// <summary>
       /// Application version no
       /// </summary>
       string Version { get; set; }
       /// <summary>
       /// Appliaction Logo
       /// </summary>
       Uri Logo { get; set; }
    }
}
