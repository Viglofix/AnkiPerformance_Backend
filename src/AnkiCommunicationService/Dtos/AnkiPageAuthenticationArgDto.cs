using Microsoft.Playwright;

namespace AnkiCommunicationService.Dtos;

/// <summary>
/// Login System DTO
/// </summary>
public record AnkiPageAuthenticationArgDto
{
    public ILocator? EmailInput { get; set; }
    public ILocator? PasswordInput { get; set; }
    public ILocator? LoginButton { get; set; }
}
