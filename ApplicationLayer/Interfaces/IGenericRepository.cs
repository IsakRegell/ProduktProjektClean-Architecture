using ApplicationLayer.Common;

namespace ApplicationLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult<IEnumerable<T>>> GetAllAsync();
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<T>> AddAsync(T entity);
        Task<OperationResult<bool>> UpdateAsync(T entity);
        Task<OperationResult<bool>> DeleteAsync(int id);
    }
}
