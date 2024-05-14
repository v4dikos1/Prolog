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

        builder.Property(x => x.DriverTransportBindId).IsRequired(false);
        builder.HasOne(x => x.DriverTransportBind)
            .WithMany()
            .HasForeignKey(x => x.DriverTransportBindId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StorageId).IsRequired();
        builder.HasOne(x => x.Storage)
            .WithMany()
            .HasForeignKey(x => x.StorageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(x => x.Coordinates).IsRequired();
        builder.Property(x => x.DeliveryDateFrom).IsRequired();
        builder.Property(x => x.PickUpDateFrom).IsRequired();
        builder.Property(x => x.PickUpDateTo).IsRequired();
        builder.Property(x => x.DeliveryDateTo).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.OrderStatus).IsRequired();

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