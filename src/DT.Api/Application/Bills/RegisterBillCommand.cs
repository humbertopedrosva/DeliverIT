using DT.Api.Application.Base;
using DT.Api.Configuration;
using FluentValidation.Results;
using System;

namespace DT.Api.Application.Bills
{
    public class RegisterBillCommand : Command
    {
        [SwaggerExclude]
        public string UserId { get;  set; }
        public string Name { get;  set; }
        public decimal OriginalValue { get;  set; }
        public DateTime DueDate { get;  set; }
        public DateTime PayDay { get;  set; }


        public RegisterBillCommand(){ }

        public RegisterBillCommand(string name, decimal originalValue, DateTime dueDate, DateTime payDay, string userId)
        {
            Name = name;
            OriginalValue = originalValue;
            DueDate = dueDate;
            PayDay = payDay;
            UserId = userId;
        }

        public override bool IsValid(IServiceProvider serviceProvider)
        {
            ValidationResult = new RegisterBillValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
