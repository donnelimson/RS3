using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Repository
{
    /// <summary>
    /// The repository base interface.
    /// </summary>
    /// <typeparam name="T">The repository type.</typeparam>
    public interface IRepositoryBase<T>
        where T : class
    {
        /// <summary>
        /// Gets the get all.
        /// </summary>
        /// <value>
        /// The get all.
        /// </value>
        IQueryable<T> GetAll { get; }

        /// <summary>
        /// Gets all including.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>All including.</returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Inserts or update the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertOrUpdate(T entity);
      
        void Insert(T entity);

        void Update(T entity);

        /// <summary>
        /// Sets the deleted entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Validation result.
        /// </returns>
        DbEntityValidationResult Validate(object entity);

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Validation result.</returns>
        DbValidationError ValidateProperty(object entity, string propertyName);

        /// <summary>
        /// Reverts the changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void RevertChanges(object entity);

        /// <summary>
        /// Determines whether the specified entity has changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity has changes; otherwise, <c>false</c>.
        /// </returns>
        bool HasChanges(object entity);

        /// <summary>
        /// Reloads the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void ReloadEntity(object entity);

        IQueryable<U> GetEntitySet<U>() where U : class;
    }
}
