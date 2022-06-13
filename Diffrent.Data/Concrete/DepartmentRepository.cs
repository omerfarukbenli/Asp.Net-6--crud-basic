using Diffrent.Data.Abstract;
using Diffrent.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Diffrent.Data.Concrete
{
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Department> Create(Department obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
            return await _context.Departments.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var date = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(date);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null)
        {
            if(filter == null)
                return await _context.Departments.ToListAsync();
            else
                return await _context.Departments.Where(filter).ToListAsync();
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter = null)
        {
            return await _context.Departments.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Department> Update(Department obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Departments.Where(a => a.Id == obj.Id).FirstOrDefaultAsync();
        }
    }
}
