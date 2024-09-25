using Mvc.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvc.Project.BLL.Interfaces
{
    public interface ISpecifications<T> where  T : ModelBase
    {
        public Expression<Func<T, bool>>? Criteria { set; get; } 
        public List<Expression<Func<T, object>>> Includes { set; get; }
    }
}
