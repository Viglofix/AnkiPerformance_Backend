using DesktopService.Dtos;

namespace DesktopService.Services.contracts;

public interface ISignalRConnection
{
    Task StartAndDisposeConnection(ForeignSentenceDto foreignSentenceDto);
}
