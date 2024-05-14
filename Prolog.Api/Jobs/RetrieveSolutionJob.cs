using MediatR;
using Prolog.Application.Orders.Commands;
using Quartz;

namespace Prolog.Api.Jobs;

public class RetrieveSolutionJob(ISender sender): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var command = new RetrieveSolutionCommand();
        await sender.Send(command, context.CancellationToken);
    }
}