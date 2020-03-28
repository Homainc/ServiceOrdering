using System;
using System.Threading;
using System.Threading.Tasks;
namespace OrderingService.Data.Interfaces
{
    public interface ISaveProvider
    {
        Task SaveAsync(CancellationToken token);
    }
}
