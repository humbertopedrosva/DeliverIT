using FluentValidation.Results;

namespace DT.Api.Responses
{
    public class CommandResponse
    {
        public object Data { get; set; }
        public bool Sucess { get; set; }
        public string Message { get; set; }

        public ValidationResult ValidationResult { get; set; }
        public CommandResponse()
        {

        }
    }
}
