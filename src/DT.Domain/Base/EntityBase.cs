using System;

namespace DT.Domain.Base
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool Removed { get; set; }
        public void SetId(Guid id) => Id = id;
        public void SetRemoved(bool removed) => Removed = removed;
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.UtcNow;
        }
    }
}
