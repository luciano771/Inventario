using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioDataAccess.Entities;
namespace InventarioDataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> GetById(int id);
        Task<TEntity> Update(int id,TEntity entity);
        Task<bool> Delete(int id);
        Task<TEntity> Create(TEntity entity);

    }
}
