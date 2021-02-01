using System;

namespace DT.Domain.Base
{
    public interface IEntity
    {
        Guid Id { get; }
        bool Removed { get; }
        void SetId(Guid id);
        void SetRemoved(bool removed);
    }
}
