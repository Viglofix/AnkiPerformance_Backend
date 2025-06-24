using AnkiCommunicationService.Dtos;
using AnkiCommunicationService.Services.Contracts;
using Microsoft.Playwright;

namespace AnkiCommunicationService.POM;


public class AnkiPageOperationsPom
{
    private readonly IPage _page;
    private readonly IAnkiPageOperations _ankiPageOperations;
    private readonly IConfiguration _config;
    public AnkiPageOperationsPom(IPage page, IAnkiPageOperations ankiPageOperations, IConfiguration config)
    {
        _page = page;
        _ankiPageOperations = ankiPageOperations;
        _config = config;
    }
    public async Task GotoAsync()
    {
        await _page.GotoAsync(_config["ANKIWEB_LOGINURL"]!);
    }
    public async Task<string> GetSentenceAsync(ForeignSentenceDto foreignSentence)
    {
       return await _ankiPageOperations.GetSentenceAsync(foreignSentence);
    }
}
