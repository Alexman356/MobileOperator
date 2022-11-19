using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace MobileOperator
{
    /// <summary>
    /// Phone numbers generator.
    /// </summary>
    public class PhoneNumbersDataGenerator : IEntityDataGenerator<Number>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbersDataGenerator"/> class.
        /// </summary>
        /// <param name="faker">Creator of phone number random data.</param>
        public PhoneNumbersDataGenerator(Faker<Number> faker)
        {
            Faker = faker;
        }

        private Faker<Number> Faker { get; }

        /// <summary>
        /// Get entities with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <returns>Entities with random unique data.</returns>
        public IEnumerable<Number> GenerateEntities(int quantity)
        {
            var entities = new List<Number>();
            for (var i = 0; i < quantity; i++)
            {
                entities.Add(GenerateNewUniqueEntityFor(entities));
            }

            return entities;
        }

        private Number GenerateNewUniqueEntityFor(List<Number> entities)
        {
            while (true)
            {
                var newEntity = GenerateNewEntity();
                if (IsUniqueNumber(newEntity, entities))
                {
                    return newEntity;
                }
            }
        }

        private Number GenerateNewEntity()
        {
            Faker.RuleFor(number => number.Number_telephone, faker => faker.Phone.PhoneNumber("+7##########"));
            var newNumber = Faker.Generate();

            return newNumber;
        }

        private bool IsUniqueNumber(Number newNumber, List<Number> numbers)
        {
            return numbers.All(number => number.Number_telephone != newNumber.Number_telephone);
        }
    }
}