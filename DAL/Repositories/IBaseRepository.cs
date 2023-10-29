using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {

        Task<T> GetSingle(string id);
    
        Task<IEnumerable<T>> GetAll();

        Task Add(T entity);

        Task Update(string id, T entity);


        Task Delete(string id);

        Task SaveChanges();
    }
}
