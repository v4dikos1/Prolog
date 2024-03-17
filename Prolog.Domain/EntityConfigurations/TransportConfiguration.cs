using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class TransportConfiguration: IEntityTypeConfiguration<Transport>
{
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.ToTable("transport");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Volume).IsRequired();
        builder.Property(x => x.Capacity).IsRequired();
        builder.Property(x => x.FuelConsumption).IsRequired();
        builder.Property(x => x.LicencePlate).IsRequired();
        builder.Property(x => x.Brand).IsRequired();

        builder.Property(x => x.ExternalSystemId).IsRequired();
        builder.HasOne(x => x.ExternalSystem)
            .WithMany()
            .HasForeignKey(x => x.ExternalSystemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsArchive).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateModified).IsRequired();
    }
}