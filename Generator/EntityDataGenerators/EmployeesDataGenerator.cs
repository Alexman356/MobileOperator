using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace MobileOperator
{
    /// <summary>
    /// Employees data generator.
    /// </summary>
    public class EmployeesDataGenerator : IEntityDataGenerator<Employee>
    {
        private readonly string[] posts = { "Онлайн консультант", "Администратор", "Оператор", "Менеджер" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbersDataGenerator"/> class.
        /// </summary>
        /// <param name="faker">Creator of employee random data.</param>
        public EmployeesDataGenerator(Faker<Employee> faker)
        {
            Faker = faker;
        }

        private Faker<Employee> Faker { get; }

        /// <summary>
        /// Get entities with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <returns>Entities with random unique data.</returns>
        public IEnumerable<Employee> GenerateEntities(int quantity)
        {
            var entities = new List<Employee>();
            for (var i = 0; i < quantity; i++)
            {
                entities.Add(GenerateNewUniqueEntityFor());
            }

            return entities;
        }

        private Employee GenerateNewUniqueEntityFor()
        {
            Faker.RuleFor(employee => employee.Salary, faker => faker.Random.Number(1, 214700))
                .RuleFor(employee => employee.Post, faker => faker.PickRandom(posts));

            var newEntity = Faker.Generate();

            return newEntity;
        }
    }
}