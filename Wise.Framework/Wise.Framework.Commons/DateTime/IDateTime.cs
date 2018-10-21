
namespace Wise.Framework.Commons.DateTime
{
    public interface IDateTime
    {
        System.DateTime Now { get; }
        System.DateTime UtcNow { get; }
    }
}
