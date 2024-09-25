using Microsoft.EntityFrameworkCore;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.DAL.Models;
using System.Linq;

namespace Mvc.Project.BLL.Repositories.Specifications
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : ModelBase
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
