using DT.Domain.Base;
using DT.Domain.Entities;
using DT.Domain.Enums;
using DT.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT.Api.Application.Bills
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IInterestRepository _interestRepository;
        public const int BUSINESS_MONTH_DAYS = 30; 

        public BillService(IBillRepository billRepository, IInterestRepository interestRepository)
        {
            _billRepository = billRepository;
            _interestRepository = interestRepository;
        }
        public async Task<List<BillModel>> GetAllBills()
        {
            var bills = await _billRepository.GetAllAsync();

            var billsModels = new List<BillModel>();
            foreach (var bill in bills)
            {

                decimal adjustedValue = bill.OriginalValue;

                if (bill.PayDay.CompareTo(bill.DueDate) > 0)
                    adjustedValue = await CalculateInterestAndPenalty(bill);


                var model = new BillModel
                {
                    Name = bill.Name,
                    NumberOfDaysLate = CalculateDaysofLate(bill),
                    OriginalValue = bill.OriginalValue,
                    PayDay = bill.PayDay,
                    AdjustedValue = adjustedValue
                };

                billsModels.Add(model);
            }

            return billsModels;
        }

        public async Task<List<BillModel>> GetAllBillsByUser(string userId)
        {
            var bills = await _billRepository.GetBillsByUserAsync(userId);

            var billsModels = new List<BillModel>();
            foreach (var bill in bills)
            {

                decimal adjustedValue = bill.OriginalValue;

                if (bill.PayDay.CompareTo(bill.DueDate) > 0)
                    adjustedValue = await CalculateInterestAndPenalty(bill);


                var model = new BillModel
                {
                    Name = bill.Name,
                    NumberOfDaysLate = CalculateDaysofLate(bill),
                    OriginalValue = bill.OriginalValue,
                    PayDay = bill.PayDay,
                    AdjustedValue = adjustedValue
                };

                billsModels.Add(model);
            }

            return billsModels;
        }

        public async Task<BillModel> GetByIdByUser(Guid id, string userId)
        {
            var bill = await _billRepository.GetByIdByUserAsync(userId, id);

            decimal adjustedValue = bill.OriginalValue;

            if (bill.PayDay.CompareTo(bill.DueDate) > 0)
                adjustedValue = await CalculateInterestAndPenalty(bill);

            return new BillModel
            {
                Name = bill.Name,
                NumberOfDaysLate = CalculateDaysofLate(bill),
                OriginalValue = bill.OriginalValue,
                PayDay = bill.PayDay,
                AdjustedValue = adjustedValue
            };
        }

        private async Task<decimal> CalculateInterestAndPenalty(Bill bill)
        {
            var interest = await _interestRepository.GetAllAsync();

            decimal adjustedValue = bill.OriginalValue;
            int numberOfDaysLate = 0;
            double interestPorcent = 0;
            double penaltyPorcent = 0;

            numberOfDaysLate = CalculateDaysofLate(bill);

            if (numberOfDaysLate <= 3)
            {
                interestPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.ATE3DIAS).InterestPorcent;
                penaltyPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.ATE3DIAS).PenaltyPorcent ;


            }

            else if (numberOfDaysLate > 3 && numberOfDaysLate < 5)
            {
                interestPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.DE4A5DIAS).InterestPorcent;
                penaltyPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.DE4A5DIAS).PenaltyPorcent;


            }

            else if (numberOfDaysLate > 5)
            {
                interestPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.SUPERIORA5DIAS).InterestPorcent;
                penaltyPorcent = interest.FirstOrDefault(x => x.DayOfDelay == DayOfDelayStatus.SUPERIORA5DIAS).PenaltyPorcent;


            }

            adjustedValue += (adjustedValue * Convert.ToDecimal(penaltyPorcent));

            adjustedValue += (adjustedValue * Convert.ToDecimal(interestPorcent)) / (BUSINESS_MONTH_DAYS * numberOfDaysLate);

            return Decimal.Round(adjustedValue, 2);
        }

        private static int CalculateDaysofLate(Bill bill)
        {
            return Convert.ToInt32(bill.PayDay.Subtract(bill.DueDate).TotalDays);
        }

      
    }
}
