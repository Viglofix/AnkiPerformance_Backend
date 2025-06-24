using AnkiCommunicationService.Dtos;
using AnkiCommunicationService.Services.Contracts;
using Microsoft.Playwright;

namespace AnkiCommunicationService.Services.Implementations;

public class AnkiPageAuthentication : IAnkiPageAuthentication
{
    private const string ArgumentExceptionMessage = "EmailInput, PasswordInput, and LoginButton cannot be null.";
    private const int AntiMagicNumberDelay = 5000;
    private const string EmailLabel = "Email";
    private const string PasswordLabel = "Password";
    private const string LoginButtonText = "button:text('Log in')";
    private readonly IPage _page;

    public AnkiPageAuthentication(IPage page)
    {
        _page = page;
    }

    public async Task LoginAsync(string email, string password)
    {
        // Fill in the email and password HTML input fields
        if (await _page.GetByLabel(EmailLabel).IsVisibleAsync() is false){return;}

        AnkiPageAuthenticationArgDto argDto = new()
        {
            EmailInput = _page.GetByLabel(EmailLabel),
            PasswordInput = _page.GetByLabel(PasswordLabel),
            LoginButton = _page.Locator(LoginButtonText)
        };


        if (argDto.EmailInput is null || argDto.PasswordInput is null || argDto.LoginButton is null)
        {
            throw new ArgumentException(ArgumentExceptionMessage);
        }

        await Task.Delay(AntiMagicNumberDelay);
        await argDto.EmailInput.FillAsync(email);
        await argDto.PasswordInput!.FillAsync(password);

        //Click the login button
        await argDto.LoginButton!.ClickAsync();
    }
}
