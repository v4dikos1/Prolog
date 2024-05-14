using Quartz;
using System.Text;

namespace Prolog.Api.Jobs.Configuration;

public static class QuartzJobsRegistrar
{
    public static IServiceCollection RegisterQuartzJobs(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(x =>
        {
            x.AddJobAndTrigger<RetrieveSolutionJob>(configuration);
        });
        services.AddQuartzHostedService(
            q => q.WaitForJobsToComplete = true);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return services;
    }
}