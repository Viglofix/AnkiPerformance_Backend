using DesktopService.Dtos;
using DesktopService.Services.contracts;
using Microsoft.AspNetCore.SignalR.Client;

namespace DesktopService.Services.implementations;

public class SignalRConnection : ISignalRConnection
{
    // Constant Data 
    private const string InvocationMethodName = "SendMessageToAllAsync";

    // Injected Reusable Services
    private readonly IHubConnectionBuilder _hubConnectionBuilder;
    private readonly IConfiguration _config;
    public SignalRConnection(IHubConnectionBuilder hubConnectionBuilder, IConfiguration config)
    {
        _hubConnectionBuilder = hubConnectionBuilder;
        _config = config;
    }
    public async Task StartAndDisposeConnection(ForeignSentenceDto foreignSentenceDto)
    {
        var value = _config["SIGNAL_R_HUB_ACCESS"];
        var hubConnection = BuildConnection();
        await EstablishConnectionAndSendMessages(hubConnection, foreignSentenceDto);
    }

    private HubConnection BuildConnection()
        => _hubConnectionBuilder
            .WithUrl(_config["SIGNAL_R_HUB_ACCESS"]!)
            .Build();

    private async Task EstablishConnectionAndSendMessages(HubConnection hubConnection, ForeignSentenceDto foreignSentenceDto) {
        try
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync(InvocationMethodName, foreignSentenceDto);
            await hubConnection.StopAsync();
        }
        finally
        {
            await hubConnection.DisposeAsync();
        }
    }
    


}
