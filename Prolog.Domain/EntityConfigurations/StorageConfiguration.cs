using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class StorageConfiguration: IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.ToTable("storage");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Address)
            .IsRequired()
            .HasColumnType("jsonb");
        builder.Property(x => x.Coordinates).IsRequired();

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