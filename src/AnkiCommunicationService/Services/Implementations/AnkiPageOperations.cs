using AnkiCommunicationService.Dtos;
using AnkiCommunicationService.Services.Contracts;
using Microsoft.Playwright;

namespace AnkiCommunicationService.Services.Implementations;

public class AnkiPageOperations : IAnkiPageOperations
{
    
    private readonly IPage _page;
    private readonly IConfiguration _config;

    public AnkiPageOperations(IPage page, IConfiguration config)
    {
        _page = page;
        _config = config;
    }

    public async Task<string> GetSentenceAsync(ForeignSentenceDto sentences)
    {

        await _page.GotoAsync(_config["LOGIN_WEBPAGE"]!);
        if (await _page.Locator("text=Add").First.IsVisibleAsync() is true)
        {
            var element = await _page.Locator("text=Add").First.InnerTextAsync();
            await _page.Locator("text=Add").ClickAsync();
            var element_tag = await _page.Locator("text=Type").First.InnerTextAsync();

            var front = _page.Locator(".form-control field").First;
            var back = _page.Locator(".form-control field").Last;

            await _page.Locator("div.form-control.field[contenteditable='true']").First.FillAsync(sentences.Sentence!);
            await _page.Locator("div.form-control.field[contenteditable='true']").Last.FillAsync(sentences.TranslatedSentence!);

            await _page.Locator(".btn.btn-primary.btn-large.mt-2").ClickAsync();

            return "Staszek malina wyslany";
        }
        return "NOT FOUND";
    }
}