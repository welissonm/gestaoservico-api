using GestaoServico.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoServico.Data{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

        public DbSet<Telefone> Telefones {get; set;}

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}