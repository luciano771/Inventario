using InventarioDataAccess.Entities;
using InventarioDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBussinessLogic.Interfaces
{
    public interface IGeneralLogic<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAll();

        Task<IQueryable<TEntity>> GetAllPereyra();
         
        Task<IQueryable<TEntity>> GetById(int id);
        Task<TEntity> Update(int id,TEntity entity);
        Task<bool> Delete(int id);
        Task<bool> Delete2(TEntity entity);
        Task<TEntity> Create(TEntity entity);
        
    }
}
