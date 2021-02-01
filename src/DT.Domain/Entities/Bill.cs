using DT.Domain.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace DT.Domain.Entities
{
    public class Bill : EntityBase
    {
        public string Name { get; protected set; }
        public decimal OriginalValue { get; protected set; }
        public DateTime DueDate { get; protected set; }
        public DateTime PayDay { get; protected set; }
        public IdentityUser User { get; protected set; }

        protected Bill(){}

        public Bill(string name, decimal originalValue, DateTime dueDate, DateTime payDay, IdentityUser user)
        {
            Name = name;
            OriginalValue = originalValue;
            DueDate = dueDate;
            PayDay = payDay;
            User = user;
        }


        public Bill AddUser(IdentityUser user)
        {
            User = user;
            return this;
        }
    }
}
