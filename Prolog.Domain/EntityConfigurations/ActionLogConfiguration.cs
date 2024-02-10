using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class ActionLogConfiguration : IEntityTypeConfiguration<ActionLog>
{
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
        builder.ToTable("action_logs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.UserSurname).IsRequired();
        builder.Property(x => x.ActionName).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.ActionDateTime).IsRequired();
        builder.Property(x => x.RequestInfo).IsRequired();
        builder.Property(x => x.IdentityUserId).IsRequired(false);

        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateModified).IsRequired();
    }
}