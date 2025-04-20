using ApplicationLayer.Common;
using ApplicationLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace InfrastructureLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //Get All ....
        public async Task<OperationResult<IEnumerable<T>>> GetAllAsync()
        {
            var data = await _dbSet.ToListAsync();
            return OperationResult<IEnumerable<T>>.Success(data);
        }

        //Get By Id
        public async Task<OperationResult<T>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null
                ? OperationResult<T>.Success(entity) //Detta är if
                : OperationResult<T>.Failure("Not found"); // Detta är else
        }

        //Skapa ny entitet till Databasen
        public async Task<OperationResult<T>> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return OperationResult<T>.Success(entity);
        }

        //Uppdatera befintlig entitet i databasen
        public async Task<OperationResult<bool>> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return OperationResult<bool>.Success(true);
        }

        public async Task<OperationResult<bool>> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return OperationResult<bool>.Failure("Not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return OperationResult<bool>.Success(true);
        }



    }
}
