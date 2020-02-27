

using System;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using GestaoServico.Data;
using GestaoServico.Models;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace GestaoServico.Service
{

    public class ServiceBase<T> where T : Entity 
    {

        protected readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;
        public ServiceBase(DataContext context)
        {
            this._context = context;
            var prop = this._context
            .GetType()
            .GetProperties()
            .Where( p => p.PropertyType == typeof (DbSet<T>))
            .SingleOrDefault();
            if(prop != null)
            {
                this._dbSet = (DbSet<T>)prop.GetValue(this._context);
            }
        }

        public virtual async Task<T> Add(T model)
        {
            if (model != null)
            {
                this._context.Add(model);
                await this._context.SaveChangesAsync();
                return model;
            }
            else
            {
                throw new ArgumentException($"O {model.EntityName()} não pode ser nulo");
            }
        }

        public virtual Task<T> FindById(long id, Expression<Func<T, object>> includes = null)
        {
            return this._dbSet
            .Include(includes)
            .AsNoTracking()
            .SingleOrDefaultAsync( x => x.Id.Equals(id));

        }
        public virtual Task<List<T>> GetAll(Expression<Func<T, object>> includes = null)
        {
            return this._dbSet
            .Include(includes)
            .AsNoTracking()
            .ToListAsync();
        }
        public virtual Task<List<T>> GetAll(Expression<Func<T, bool>> expression , Expression<Func<T, object>> includes = null)
        {
            return this._dbSet
            .Include(includes)
            .AsNoTracking()
            .Where(expression)
            .ToListAsync();
        }

        public async virtual Task<T> Update(T entity){
            this._dbSet.Update(entity);
            var rowNumbers = await this._context.SaveChangesAsync();
            return entity;
        }
    }
}