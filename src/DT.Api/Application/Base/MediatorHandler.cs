using DT.Api.Responses;
using MediatR;
using System.Threading.Tasks;

namespace DT.Api.Application.Base
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CommandResponse> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
