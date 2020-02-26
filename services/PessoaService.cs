using System.Threading.Tasks;
using GestaoServico.Data;
using GestaoServico.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoServico.Service{

    public class PessoaService : ServiceBase<Pessoa>{
        public PessoaService(DataContext context): base(context)
        {
            
        }
        public Task<Pessoa> GetPessoa(int id){
            return this._context.Pessoas
            .Include( x => x.Contato)
            .AsNoTracking()
            .SingleOrDefaultAsync( x => x.Id == id); 
        }

    }
}