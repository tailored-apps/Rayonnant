using System.Threading.Tasks;

namespace Wise.Framework.Interface.MultiThreading
{
    public interface ITaskCreator
    {
        Task CreateTask();
    }
}