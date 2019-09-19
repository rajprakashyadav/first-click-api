using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PERFECT.CLICK.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includePaths">The include paths.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortingDetails">The sorting details.</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = 0,
           int? pageSize = null,
           string sortingDetails = null,
           string searchFilter = null);

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="filterString">The filter string.</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> filter = null, string filterString = "");

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="addInline">if set to <c>true</c> [add inline].</param>
        /// <returns></returns>
        TEntity Add(TEntity entity, bool addInline = false);
        List<TEntity> AddRange(List<TEntity> entities);
        /// <summary>
        /// Alters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Alter(TEntity entity);
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        void ExecuteCommand(string command);
    }
}
