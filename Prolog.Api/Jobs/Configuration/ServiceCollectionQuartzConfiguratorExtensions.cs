﻿using Quartz;

namespace Prolog.Api.Jobs.Configuration;

public static class ServiceCollectionQuartzConfiguratorExtensions
{
    public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config)
        where T : IJob
    {
        // Use the name of the IJob as the appsettings.json key
        var jobName = typeof(T).Name;

        // Try and load the schedule from commonConfiguration
        var configKey = $"Quartz:{jobName}";
        var cronSchedule = config[configKey];

        // Some minor validation
        if (string.IsNullOrEmpty(cronSchedule))
        {
            return;
        }

        // register the job as before
        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronSchedule)); // use the schedule from commonConfiguration
    }
}