using System.Threading;
using System.Threading.Tasks;

namespace TailoredApps.Rayonnant.Interface.MultiThreading
{
    public interface ITaskCreator
    {
        Task CreateTask(CancellationTokenSource cancelationToken);
    }
}