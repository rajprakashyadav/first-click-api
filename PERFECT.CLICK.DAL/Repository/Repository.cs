using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;

namespace PERFECT.CLICK.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private Fields

        /// <summary>
        /// The database
        /// </summary>
        private DbContext _db;

        /// <summary>
        /// The database set
        /// </summary>
        private DbSet<TEntity> _dbSet;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            _db = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion Constructor

        #region Interface Implementation Methods

        /// <summary>
        /// Method to Add an Entity
        /// </summary>
        /// <param name="entity">Entity object to be added</param>
        /// <param name="addInline">if set to <c>true</c> [add in-line].</param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity, bool addInline = false)
        {
            try
            {
                _dbSet.Add(entity);

                if (addInline)
                {
                    _db.SaveChanges();
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to Add list of entities
        /// </summary>
        /// <param name="entity">Entity object to be added</param>
        /// <param name="addInline">if set to <c>true</c> [add in-line].</param>
        /// <returns></returns>
        public virtual List<TEntity> AddRange(List<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                return entities;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to update and entity
        /// </summary>
        /// <param name="entity">Entity Object to be updated</param>
        public virtual void Alter(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get all entities
        /// </summary>
        /// <param name="orderBy">Order by clauses - leave blank for no ordering</param>
        /// <param name="includeProperties">Properties to be included with the result set - leave blank if not required</param>
        /// <returns>
        /// All entities
        /// </returns>
        public IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                return _dbSet.AsNoTracking();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to remove an entity
        /// </summary>
        /// <param name="entity">Entity object to be removed</param>
        public virtual void Remove(TEntity entity)
        {
            try
            {
                if (_db.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get filtered entity list with server side pagination enabled
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includePaths">The include paths.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortingDetails">The sorting details.</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = 0,
           int? pageSize = null,
           string sortingDetails = null,
           string searchFilter = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.AsNoTracking();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includePaths != null)
                {
                    for (var path = 0; path < includePaths.Count(); path++)
                    {
                        query = query.Include(includePaths[path]);
                    }
                }

                // At least one sort expression is needed for the page parameter to be considered
                // Skip() will throw an exception if the query is not ordered
                if (!string.IsNullOrEmpty(sortingDetails))
                {
                    query = query.OrderBy(sortingDetails);

                    if (page != null)
                    {
                        query = query.Skip((int)page);
                    }
                }

                if (pageSize != null)
                {
                    query = query.Take((int)pageSize);
                }

                if (!string.IsNullOrEmpty(searchFilter))
                {
                    query = query.Where(searchFilter);
                }

                var sql = query.ToString();

                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="filterString">The filter string.</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> filter = null, string filterString = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(filterString))
                {
                    return _dbSet.Where(filterString).Count();
                }
                if (filter == null)
                {
                    return _dbSet.Count();
                }

                return _dbSet.Where(filter).Count();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void ExecuteCommand(string command)
        {
            try
            {
                _db.Database.ExecuteSqlCommand(command);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
