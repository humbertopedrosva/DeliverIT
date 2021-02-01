using DT.Domain.Base;
using DT.Domain.Enums;

namespace DT.Domain.Entities
{
    public class Interest : EntityBase
    {
        public DayOfDelayStatus DayOfDelay { get; protected set; }
        public double PenaltyPorcent { get; protected set; }
        public double InterestPorcent { get; protected set; }

        protected Interest(){}
        public Interest(DayOfDelayStatus dayOfDelay, double penaltyPorcent, double interestPorcent)
        {
            DayOfDelay = dayOfDelay;
            PenaltyPorcent = penaltyPorcent;
            InterestPorcent = interestPorcent;
        }
    }
}
