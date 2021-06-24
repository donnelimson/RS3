using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Repository Base class.
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    /// <seealso cref="IRepositoryBase{T}" />
    /// <seealso cref="System.IDisposable" />
    public abstract class RepositoryBase<T> : IRepositoryBase<T>, IDisposable
        where T : class
    {
        #region Fields

        private readonly DbContext context;
        private bool disposed = false;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RepositoryBase(DbContext context)
        {
            this.context = context;
            this.context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <returns>The data set.</returns>
        //protected abstract IDbSet<T> GetDataSet();

        /// <summary>
        /// Inserts or update the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void InsertOrUpdate(T entity);
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected DbContext Context
        {
            get { return this.context; }
        }

        #region IRepositoryBase

        /// <summary>
        /// Gets the get all.
        /// </summary>
        /// <value>
        /// The get all.
        /// </value>
        public IQueryable<T> GetAll
        {
            get
            {
                //return this.GetDataSet();
                return this.Context.Set<T>();
            }
        }

        /// <summary>
        /// Gets all including.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>All including.</returns>
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            //IQueryable<T> query = this.GetDataSet();
            IQueryable<T> query = this.Context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }

        /// <summary>
        /// Sets the deleted entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            if (this.Context.Entry(entity).State != EntityState.Detached)
            {
                this.Context.Entry(entity).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Collection of validation results.
        /// </returns>
        public DbEntityValidationResult Validate(object entity)
        {
            return this.Context.Entry(entity).GetValidationResult();
        }

        /// <summary>
        /// Validates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Property validation error.</returns>
        public DbValidationError ValidateProperty(object entity, string propertyName)
        {
            var validationErrors = this.Context.Entry(entity).Property(propertyName).GetValidationErrors();
            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                {
                    return error;
                }
            }

            return null;
        }

        /// <summary>
        /// Reverts the changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void RevertChanges(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entry = this.Context.Entry(entity);
            if (entry != null)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified entity has changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if there are changes, otherwise false.</returns>
        public bool HasChanges(object entity)
        {
            this.Context.ChangeTracker.DetectChanges();
            var entry = this.Context.Entry(entity);
            return entry.State == EntityState.Modified;
        }

        /// <summary>
        /// Reloads the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void ReloadEntity(object entity)
        {
            this.Context.Entry(entity).Reload();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>True</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }

        /// <summary>
        /// Releases the resources used by the repository.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Checks the disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(null);
            }
        }

        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets the get all.
        /// </summary>
        /// <value>
        /// The get all.
        /// </value>
        public IQueryable<U> GetEntitySet<U>() where U : class
        {
            return Context.Set<U>().AsQueryable();
        }

        #endregion
    }
}
