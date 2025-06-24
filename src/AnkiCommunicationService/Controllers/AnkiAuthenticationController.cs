using Microsoft.AspNetCore.Mvc;
using AnkiCommunicationService.POM;
using AnkiCommunicationService.Dtos;

namespace AnkiCommunicationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnkiAuthenticationController : ControllerBase
    {
        private const string ReturnMessage = "Login successful";
        private readonly AnkiPageAuthenticationPom _ankiPageAuthenticationPom;
        public AnkiAuthenticationController(AnkiPageAuthenticationPom ankiPageAuthenticationPom)
        {
            _ankiPageAuthenticationPom = ankiPageAuthenticationPom;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDataDto loginDataDto)
        {
            await _ankiPageAuthenticationPom.GotoAsync();
            await _ankiPageAuthenticationPom.LoginAsync(loginDataDto.Login!, loginDataDto.Password!);

            return Ok(ReturnMessage);
        }
    }
}
