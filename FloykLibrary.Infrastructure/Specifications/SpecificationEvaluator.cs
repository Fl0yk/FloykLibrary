using FloykLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FloykLibrary.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(
            IQueryable<TEntity> inputQuerable,
            Specification<TEntity> specification)
            where TEntity : Entity
        {
            IQueryable<TEntity> querable = inputQuerable;

            if (specification.Criteria is not null)
            {
                querable = querable.Where(specification.Criteria);
            }

            querable = specification.IncludeExpressions.Aggregate(
                querable,
                (current, includeExpression) =>
                    current.Include(includeExpression));

            if (specification.OrderByExpression is not null)
            {
                querable = querable.OrderBy(specification.OrderByExpression);
            }
            else if (specification.OrderByDescendingExpression is not null)
            {
                querable = querable.OrderByDescending(specification.OrderByDescendingExpression);
            }

            if (specification.IsSplitQuery)
            {
                querable = querable.AsSplitQuery();
            }

            return querable;
        }
    }
}
