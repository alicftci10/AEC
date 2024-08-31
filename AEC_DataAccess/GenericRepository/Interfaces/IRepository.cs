using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.GenericRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        T Update(T entity);

        T Delete(int pId);

        T GetSelect(int pId);

        IEnumerable<T> GetAll();
    }
}
