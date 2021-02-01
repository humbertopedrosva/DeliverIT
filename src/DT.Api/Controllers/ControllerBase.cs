using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DT.Api.Controllers
{
    [ApiController]
    public class ControllerBase : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponseErrorValidation(IList<ValidationFailure> errors)
        {

            foreach (var error in errors)
                AddError($"{error.PropertyName} - {error.ErrorMessage}");


            return CustomResponse();
        }

        protected bool OperationValid()
        {
            return !Erros.Any();
        }

        protected void AddError(string erro)
        {
            Erros.Add(erro);
        }

        protected void CleanErrors()
        {
            Erros.Clear();
        }
    }
}


