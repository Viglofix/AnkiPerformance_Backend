using Microsoft.AspNetCore.SignalR;

namespace BffService.Hubs;

public class AnkiHub : Hub
{
    private const string MethodName = "Anki";
    public async Task SendMessageToAllAsync(object messages)
    {
        await Clients.All.SendAsync(MethodName, messages);
    }
}

