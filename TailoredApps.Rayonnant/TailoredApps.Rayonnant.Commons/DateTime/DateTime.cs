
namespace TailoredApps.Rayonnant.Commons.DateTime
{
    public class DateTime :  IDateTime
    {
        public System.DateTime Now { get { return System.DateTime.Now; } }
        public System.DateTime UtcNow { get { return System.DateTime.UtcNow; } }
    }
}
