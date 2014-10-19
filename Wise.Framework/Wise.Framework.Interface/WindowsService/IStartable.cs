using System.Threading.Tasks;

namespace Wise.Framework.Interface.WindowsService
{
    public interface IStartable
    {
        void Start();
        void Start(TaskScheduler scheduler);
    }
}