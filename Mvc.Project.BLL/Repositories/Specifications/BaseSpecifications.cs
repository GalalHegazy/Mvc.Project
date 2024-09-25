using Mvc.Project.BLL.Interfaces;
using Mvc.Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mvc.Project.BLL.Repositories.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : ModelBase
    {
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, bool>> Criteria { get; set; }

        public BaseSpecifications()
        { 
        }
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
    }
}
