using Microsoft.EntityFrameworkCore;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.DAL.Data;
using Mvc.Project.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Mvc.Project.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        //public IQueryable<Employee> GetEmployeeByAddress(string address)
        //{
        //    return _dbContext.Employees.Where(e=>e.Address.ToLower() == address.ToLower());
        //}

        //public IQueryable<Employee> SearchByName(string name)
        //{
            
        //    return _dbContext.Employees.Where(e => e.Name.ToLower().Contains(name))
        //                               .Include(e => e.Department).AsNoTracking();
        //}
    }
}
