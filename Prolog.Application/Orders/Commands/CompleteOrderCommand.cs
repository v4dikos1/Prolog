using MediatR;

namespace Prolog.Application.Orders.Commands;

public class CompleteOrderCommand: IRequest
{
    public long OrderId { get; set; }
}