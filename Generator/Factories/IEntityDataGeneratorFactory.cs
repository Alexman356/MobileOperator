namespace MobileOperator
{
    /// <summary>
    /// Interface of any entity data generator factory.
    /// </summary>
    public interface IEntityDataGeneratorFactory
    {
        /// <summary>
        /// Get special entity data generator.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <returns>Special entity data generator.</returns>
        IEntityDataGenerator<IEntity> GetEntityDataGenerator<TEntity>();
    }
}