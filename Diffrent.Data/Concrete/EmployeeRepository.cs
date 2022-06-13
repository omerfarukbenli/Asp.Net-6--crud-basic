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
    public class EmployeeRepository : IGenericRepository<Employee>
    {

        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(Employee obj)
        {
            await _context.AddAsync(obj);
            await _context.SaveChangesAsync();
            return await _context.Employees.OrderByDescending(a=>a.Id).Include("Department").FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
           var date = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(date);
            await _context.SaveChangesAsync();      
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> filter = null)
        {

            if (filter == null)
                return await _context.Employees.Include("Department").ToListAsync();
            else
                return await _context.Employees.Where(filter).Include("Department").ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter = null)
        {
            return await _context.Employees.Where(filter).Include("Department").FirstOrDefaultAsync();
        }

        public async Task<Employee> Update(Employee obj)
        {
           _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Employees.Where(a => a.Id == obj.Id).Include("Department").FirstOrDefaultAsync();
        }
    }
}
