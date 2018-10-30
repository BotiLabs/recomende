using Eudora.Api.Models;
using Eudora.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagensController : ControllerBase
    {
        private readonly IMensagemService _mensagemService;
        private readonly ILogger<MensagensController> _logger;

        public MensagensController(IMensagemService mensagemService, ILogger<MensagensController> logger)
        {
            _mensagemService = mensagemService;
            _logger = logger;
        }

        // GET api/mensagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mensagem>>> Get()
        {
            var mensagens = await _mensagemService.GetAll();

            return new List<Mensagem>(mensagens);
        }

        // POST api/mensagens
        [HttpPost]
        public async Task<ActionResult> Add([FromForm]Mensagem mensagem)
        {
            await _mensagemService.Add(mensagem);

            return Ok();
        }
    }
}