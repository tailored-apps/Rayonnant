using System;
using System.Linq;
using Microsoft.Practices.Prism.Regions;

namespace Wise.Framework.Presentation.Modularity
{
    public class NavigationRequest
    {
        public NavigationRequest()
        {
            screenId = Guid.NewGuid().ToString();
        }
        private NavigationParameters uriQuery;
        private string screenId = string.Empty;
        public string ScreenId { get { return screenId; } }
        public string ViewModelFullName { get; set; }
        public Type ViewModelType { get; set; }

        public NavigationParameters UriQuery
        {
            get { return uriQuery; }
            set
            {
                uriQuery = value;
                screenId = uriQuery.Any(x=>"ScreenId".Equals(x.Key)) ? uriQuery["ScreenId"].ToString() : Guid.NewGuid().ToString();
            }
        }

        public string RegionName { get; set; }
        public bool IsModal { get; set; }
        public bool IsModalyLocked { get; set; }
        public Int32 ParentWindowHandlerId { get; set; }
    }
}