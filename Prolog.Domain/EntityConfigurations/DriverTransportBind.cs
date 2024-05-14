using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prolog.Domain.EntityConfigurations;

internal class DriverTransportBind: IEntityTypeConfiguration<Entities.DriverTransportBind>
{
    public void Configure(EntityTypeBuilder<Entities.DriverTransportBind> builder)
    {
        builder.ToTable("driver_transport_bind");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.DriverId).IsRequired();
        builder.HasOne(x => x.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.TransportId).IsRequired();
        builder.HasOne(x => x.Transport)
            .WithMany()
            .HasForeignKey(x => x.TransportId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();

        builder.Property(x => x.ProblemId).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateModified).IsRequired();
    }
}