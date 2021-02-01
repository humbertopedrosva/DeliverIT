using DT.Api.Responses;
using System.Threading.Tasks;

namespace DT.Api.Application.Base
{
    public interface IMediatorHandler
    {
        Task<CommandResponse> SendCommand<T>(T command) where T : Command;
    }
}
