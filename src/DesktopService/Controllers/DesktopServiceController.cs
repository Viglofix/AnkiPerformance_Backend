using DesktopService.Dtos;
using DesktopService.Services.contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class DesktopServiceController : ControllerBase
{
    // Constant Data
    private const string ReturnMessage = "Object has been created";

    // Inject Reusable Services
    private readonly IDesktopService _desktopService;
    public DesktopServiceController(IDesktopService desktopService)
    {
        _desktopService = desktopService;
    }

    [HttpPost("add")]
    public async Task<ActionResult<ForeignSentenceDto>> Add([FromBody] ForeignSentenceDto foreignSentenceDto)
    {
        var obj = await _desktopService.Add(foreignSentenceDto);
        return Created(ReturnMessage, foreignSentenceDto);
    }
}