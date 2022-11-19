using System.Collections.Generic;

namespace MobileOperator
{
    /// <summary>
    /// Generator of entities with random unique data for database MobileOperator2022.
    /// </summary>
    public class EntityGenerator
    {
        /// <summary>
        /// Generate entities with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <returns>Entities with random unique data.</returns>
        public IEnumerable<TEntity> Generate<TEntity>(int quantity)
        where TEntity : IEntity, new()
        {
            var entityGeneratorFactory = new EntityDataGeneratorFactory();
            var entityGenerator = entityGeneratorFactory.GetEntityDataGenerator<TEntity>();
            var entities = entityGenerator.GenerateEntities(quantity);

            return entities as IEnumerable<TEntity>;
        }
    }
}