

using System.Threading.Tasks;
using GestaoServico.Models;
using GestaoServico.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoServico.Controllers{
    public abstract class AbstractController<T> : ControllerBase where T : IEntity {
        protected ServiceBase<T> _service;

        public AbstractController(ServiceBase<T> service){
            this._service = service;
        }

        [HttpPost]
        public async Task<ActionResult<T>> Create([FromBody] T model)
        {
            if(ModelState.IsValid){
                await this._service.Add(model);
                return model;
            }
            return BadRequest(ModelState);
        }
    }
}