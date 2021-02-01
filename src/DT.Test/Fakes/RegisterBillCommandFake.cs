using DT.Api.Application.Bills;
using System;

namespace DT.Test.Fakes
{
    public static class RegisterBillCommandFake
    {
        public const string Name = "Luz";
        public const decimal OriginalValue = 350;
        public static DateTime DueDate = DateTime.UtcNow;
        public static DateTime PayDay = DateTime.UtcNow;
        public const string UserId = "46e72492-c8b3-452f-ae8e-a1b32e58063b";

        public static RegisterBillCommand Default()
        {
            return new RegisterBillCommand(Name, OriginalValue, DueDate, PayDay, UserId);
        }
    }
}
