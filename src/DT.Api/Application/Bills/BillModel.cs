using System;

namespace DT.Api.Application.Bills
{
    public class BillModel
    {
        public string Name { get; set; }
        public decimal OriginalValue { get; set; }
        public decimal AdjustedValue { get; set; }
        public int NumberOfDaysLate  { get; set; }
        public DateTime PayDay { get;  set; }
    }
}
