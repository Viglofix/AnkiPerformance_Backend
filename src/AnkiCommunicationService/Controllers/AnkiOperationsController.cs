using AnkiCommunicationService.Dtos;
using AnkiCommunicationService.POM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnkiCommunicationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnkiOperationsController : ControllerBase
    {
        private readonly AnkiPageOperationsPom _ankiPageOperationsPom;
        public AnkiOperationsController(AnkiPageOperationsPom ankiPageOperationsPom)
        {
            _ankiPageOperationsPom = ankiPageOperationsPom;
        }

        [Authorize]
        [HttpPost("getMessage")]
        public ActionResult<string> GetMessage()
        {
            return Ok("siema eniu");
        }

        [Authorize]
        [HttpPost("send_sentence")]
        public async Task<ActionResult<string>> GetSentence([FromBody] ForeignSentenceDto sentences)
        {
            return await _ankiPageOperationsPom.GetSentenceAsync(sentences);
        }
    }
}
