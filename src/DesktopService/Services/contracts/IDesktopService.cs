using DesktopService.Dtos;

namespace DesktopService.Services.contracts;

public interface IDesktopService
{
    Task<ForeignSentenceDto> Add(ForeignSentenceDto foreignSentenceDto);
}
