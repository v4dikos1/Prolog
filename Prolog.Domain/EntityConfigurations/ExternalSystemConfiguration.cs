using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class ExternalSystemConfiguration: IEntityTypeConfiguration<ExternalSystem>
{
    public void Configure(EntityTypeBuilder<ExternalSystem> builder)
    {
        builder.ToTable("external_system");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.IdentityId).IsRequired();
        builder.HasIndex(x => x.IdentityId).IsUnique();

        builder.Property(x => x.IsArchive).IsRequired();
        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateModified).IsRequired();
    }
}