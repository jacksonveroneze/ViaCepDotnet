using System.Collections.Generic;
using System.Threading.Tasks;

namespace JacksonVeroneze.ViaCep.BuildingBlocks
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<List<T>> FindAllAsync();

        Task<T> FindAsync(int id);
    }
}
