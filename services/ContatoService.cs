using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GestaoServico.Data;
using GestaoServico.Models;
using System;

namespace GestaoServico.Service {

    public class ContatoService : ServiceBase<Telefone> {
        public ContatoService(DataContext context): base(context)
        {
            
        }
        public async Task<List<Telefone>> GetContato(Telefone contato)
        {
            return await this._context.Telefones
            .AsNoTracking()
            .Where( x => x.Equals(contato))
            .ToListAsync();
        }
        public async Task<Telefone> GetContato(long contatoId)
        {
            return await this._context.Telefones
            .AsNoTracking()
            .FirstOrDefaultAsync( x => x.Id == contatoId);
        }

        public async Task<List<Telefone>> GetContatos(long pessoaId)
        {
            return await this._context.Telefones
            .AsNoTracking()
            .Where( x => x.PessoaId == pessoaId)
            .ToListAsync();
        }

        public async Task<List<Telefone>> GetContatos(Pessoa pessoa)
        {
            return await this.GetContatos(pessoa.Id);
        }

    }
}