using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Diffrent.Data.Abstract
{
    public interface IGenericRepository<Tentity>
    {
        Task<IEnumerable<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> filter = null);
        Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>> filter = null);
        Task<Tentity> Create(Tentity obj);
        Task<Tentity> Update(Tentity obj);
        Task Delete(int id);
    }
}
