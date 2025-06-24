using Microsoft.Playwright;

namespace AnkiCommunicationService.Drivers;

/// <summary>
/// Reusable Driver Sigleton
/// </summary>
public class AnkiDriver : IDisposable
{
    private const string PersistLocation = @"C:\Programming\AnkiPerformance_Project\persist";

    private readonly Task<IPage> _page;
    private IBrowserContext? _browser;
    public IPage Page => _page.Result;
    private AnkiDriver()
    {
        _page = CreatePageAsync();
    }
    public static AnkiDriver CreateAnkiDriver()
    {
        return new AnkiDriver();
    }
    private async Task<IPage> CreatePageAsync()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchPersistentContextAsync(PersistLocation,
        new()
        {
            Headless = true, // Set to true if you want to run in headless mode
            SlowMo = 800 // Optional: slows down operations for debugging
        });
        return await _browser.NewPageAsync();
    }

    public void Dispose()
    {
        _browser?.CloseAsync();
    }
}
