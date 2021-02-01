using DT.Api.Configuration;
using DT.Api.Responses;
using FluentValidation.Results;
using MediatR;
using System;

namespace DT.Api.Application.Base
{
    public abstract class Command : Message, IRequest<CommandResponse>
    {
        public DateTime Timestamp { get; protected set; }

        [SwaggerExclude]
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public virtual bool IsValid(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
