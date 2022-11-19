using System;
using Bogus;

namespace MobileOperator
{
    /// <inheritdoc />
    public class EntityDataGeneratorFactory : IEntityDataGeneratorFactory
    {
        /// <inheritdoc/>
        public IEntityDataGenerator<IEntity> GetEntityDataGenerator<TEntity>()
        {
            switch (typeof(TEntity).Name)
            {
                case "Number":
                {
                    return new PhoneNumbersDataGenerator(new Faker<Number>());
                }

                case "Employee":
                {
                    return new EmployeesDataGenerator(new Faker<Employee>());
                }

                case "Person":
                {
                    return new PersonsDataGenerator(new Faker<Person>("ru"));
                }

                case "User":
                {
                    return new UsersDataGenerator(new Faker<User>("ru"));
                }

                default:
                {
                    throw new ArgumentException("Invalid type of data generator!");
                }
            }
        }
    }
}