using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Core.Repositories
{
    // TEntity คือ "คลาสอะไรก็ได้" เช่น Asset, Supplier
    public interface IRepository<TEntity> where TEntity : class
    {
        // Get by Primary Key (เราใช้ Guid)
        Task<TEntity?> GetByIdAsync(Guid id);
        // Get all
        Task<IEnumerable<TEntity>> GetAllAsync();
        // Add
        Task CreateAsync(TEntity entity);
        // Update (เมธอดนี้มักจะเป็น Sync เพราะแค่เปลี่ยน State)
        void Update(TEntity entity);
        // Delete (เมธอดนี้มักจะเป็น Sync เพราะแค่เปลี่ยน State)
        void Delete(TEntity entity);
    }
}
