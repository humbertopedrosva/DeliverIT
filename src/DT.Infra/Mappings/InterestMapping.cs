using DT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DT.Infra.Mappings
{
    public class InterestMapping : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayOfDelay).IsRequired();
            builder.Property(x => x.InterestPorcent).IsRequired();
            builder.Property(x => x.PenaltyPorcent).IsRequired();

            builder.HasIndex(x => x.CreateAt);
        }
    }
}
