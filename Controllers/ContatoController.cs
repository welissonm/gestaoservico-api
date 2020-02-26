using System.Threading.Tasks;
using GestaoServico.Models;
using GestaoServico.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoServico.Controllers {
    
    [ApiController]
    [Route("v1/contatos")]
    public class ContatoController : ControllerBase {
        private readonly ContatoService service;

        public ContatoController([FromServices] ContatoService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Telefone>> Create([FromBody] Telefone contato)
        {
            if(ModelState.IsValid){
                await this.service.Add(contato);
                return contato;
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Telefone>> Contato(long id)
        {
            var tel =  await this.service.GetContato(id);
            if(tel == null){
                return NotFound();
            }
            return tel;
        }
    }
}