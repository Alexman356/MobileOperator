using System.Collections.Generic;

namespace MobileOperator
{
    /// <summary>
    /// Special entity random unique data generator.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IEntityDataGenerator<out TEntity>
    {
        /// <summary>
        /// Generate new entity with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <returns>New entity with random unique data.</returns>
        IEnumerable<TEntity> GenerateEntities(int quantity);
    }
}