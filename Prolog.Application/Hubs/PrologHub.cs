using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Prolog.Application.Hubs;

[AllowAnonymous]
[EnableCors("SignalR")]
public class PrologHub: Hub<IPrologHub>
{
}