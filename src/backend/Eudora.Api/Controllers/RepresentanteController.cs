using Eudora.Api.Models;
using Eudora.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eudora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentanteController : ControllerBase
    {
        private readonly IRepresentanteService _representanteService;

        public RepresentanteController(IRepresentanteService representanteService)
        {
            _representanteService = representanteService;
        }

        // GET api/representante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Representante>>> Get()
        {
            var representantes = await _representanteService.GetAll();

            return new List<Representante>(representantes);
        }

        // GET api/representante/dashboard/{codRepresentante}
        [HttpGet("dashboard/{codRepresentante}")]
        public async Task<ActionResult<RepresentanteDashboard>> GetDashboard(int codRepresentante)
        {
            return await _representanteService.GetDashboard(codRepresentante);
        }

        // POST api/representante
        [HttpPost("populate")]
        public async Task<ActionResult> Populate()
        {
            var representantes = await _representanteService.Populate();

            return Ok();
        }
    }
}