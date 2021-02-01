using DT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DT.Infra.Mappings
{
    public class BillMapping : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DueDate).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.OriginalValue).IsRequired();
            builder.Property(x => x.PayDay).IsRequired();
        }
    }
}
