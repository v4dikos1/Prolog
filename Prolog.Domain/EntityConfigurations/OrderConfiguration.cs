using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class OrderConfiguration: IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CustomerId).IsRequired();
        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DriverId).IsRequired(false);
        builder.HasOne(x => x.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.TransportId).IsRequired(false);
        builder.HasOne(x => x.Transport)
            .WithMany()
            .HasForeignKey(x => x.TransportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.PaymentType).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Discount).IsRequired();

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