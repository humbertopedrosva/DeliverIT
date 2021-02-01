using DT.Domain.Entities;
using DT.Domain.Interfaces;
using DT.Infra.Context;

namespace DT.Infra.Repositories
{
    public class InterestRepository : RepositoryBase<Interest>, IInterestRepository
    {
        public InterestRepository(DTContext context) : base(context)
        {

        }
    }
}
