using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prolog.Domain.Entities;

namespace Prolog.Domain.EntityConfigurations;

internal class ProblemSolutionConfiguration: IEntityTypeConfiguration<ProblemSolution>
{
    public void Configure(EntityTypeBuilder<ProblemSolution> builder)
    {
        builder.ToTable("problem_solution");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.LocationId)
            .IsRequired();
        builder.Property(x => x.StopType).IsRequired();
        builder.Property(x => x.VehicleId).IsRequired();
        builder.Property(x => x.Index).IsRequired();
        builder.Property(x => x.Latitude).IsRequired();
        builder.Property(x => x.Longitude).IsRequired();
        builder.Property(x => x.ProblemId).IsRequired();

        builder.Property(x => x.DateCreated).IsRequired();
        builder.Property(x => x.DateModified).IsRequired();
    }
}