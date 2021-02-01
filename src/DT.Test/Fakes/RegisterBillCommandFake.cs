using DT.Api.Application.Bills;
using System;

namespace DT.Test.Fakes
{
    public static class RegisterBillCommandFake
    {
        public const string Name = "Doces";
        public const decimal OriginalValue = 25;
        public static DateTime DueDate = new DateTime(2021, 2,1);
        public static DateTime PayDay = new DateTime(2021, 2, 1);
        public const string UserId = "46e72492-c8b3-452f-ae8e-a1b32e58063b";

        public static RegisterBillCommand Default()
        {
            return new RegisterBillCommand(Name, OriginalValue, DueDate, PayDay, UserId);
        }
    }
}
