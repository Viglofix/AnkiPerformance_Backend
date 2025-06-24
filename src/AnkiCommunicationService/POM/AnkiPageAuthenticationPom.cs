using AnkiCommunicationService.Services.Contracts;
using Microsoft.Playwright;

namespace AnkiCommunicationService.POM;

public class AnkiPageAuthenticationPom
{
    private readonly IPage _page;
    private readonly IAnkiPageAuthentication _ankiPageAuthentication;
    private readonly IConfiguration _config;
    public AnkiPageAuthenticationPom(IPage page, IAnkiPageAuthentication ankiPageAuthentication, IConfiguration config)
    {
        _page = page;
        _ankiPageAuthentication = ankiPageAuthentication;
        _config = config;
    }
    public async Task GotoAsync()
    {
        await _page.GotoAsync(_config["ANKIWEB_LOGINURL"]!);
    }
    public async Task LoginAsync(string email, string password)
    {
        await _ankiPageAuthentication.LoginAsync(email, password);
    }

}
