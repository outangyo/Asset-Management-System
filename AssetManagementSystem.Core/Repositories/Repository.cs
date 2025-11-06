using AssetManagementSystem.Db.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>(); // <--- นี่คือคำสั่งสำคัญ
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            // .FindAsync จะค้นหาจาก Primary Key
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            // แค่บอก Context ว่าตัวนี้ "ถูกแก้ไข"
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            // แค่บอก Context ว่าตัวนี้ "ถูกลบ"
            _dbSet.Remove(entity);
        }
    }
}
