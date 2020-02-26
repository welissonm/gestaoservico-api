
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoServico.Data;
using GestaoServico.Models;
using GestaoServico.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoServico.Controllers {
    
    [ApiController]
    [Route("v1/pessoas")]
    public class PessoaController : ControllerBase {

        private readonly PessoaService service;

        public PessoaController([FromServices] PessoaService service){
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> Create([FromBody] Pessoa pessoa)
        {
            if(ModelState.IsValid){
                await this.service.Add(pessoa);
                return pessoa;
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> GetAll(){
            var pessoas = await this.service.GetAll();
            return pessoas;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pessoa>> Get(int id)
        {
            var pessoa = await this.service.GetPessoa(id);
            return pessoa;
        }       

    }

}