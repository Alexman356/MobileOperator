using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace MobileOperator
{
    /// <summary>
    /// Users data generator.
    /// </summary>
    public class UsersDataGenerator : IEntityDataGenerator<User>
    {
        private readonly string[] roles = { "Администратор", "Оператор", "Абонент" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbersDataGenerator"/> class.
        /// </summary>
        /// <param name="faker">Creator of user random data.</param>
        public UsersDataGenerator(Faker<User> faker)
        {
            Faker = faker;
        }

        private Faker<User> Faker { get; }

        /// <summary>
        /// Get entities with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <returns>Entities with random unique data.</returns>
        public IEnumerable<User> GenerateEntities(int quantity)
        {
            var entities = new List<User>();
            for (var i = 0; i < quantity; i++)
            {
                entities.Add(GenerateNewUniqueEntityFor(entities));
            }

            return entities;
        }

        private User GenerateNewUniqueEntityFor(List<User> entities)
        {
            while (true)
            {
                var newEntity = GenerateNewEntity();
                if (IsUniqueUser(newEntity, entities))
                {
                    return newEntity;
                }
            }
        }

        private User GenerateNewEntity()
        {
            Faker.RuleFor(user => user.Login, faker => faker.Lorem.Words()[0] + faker.Lorem.Words()[1] + faker.Lorem.Words()[2])
                .RuleFor(user => user.Role, faker => faker.PickRandom(roles))
                .RuleFor(user => user.PasswordHash, faker => faker.Random.Hash(44, true))
                .RuleFor(user => user.PasswordSalt, faker => faker.Random.Utf16String(1, 10));

            var newUser = Faker.Generate();

            return newUser;
        }

        private bool IsUniqueUser(User newUser, List<User> users)
        {
            return users.All(user => user.Login != newUser.Login);
        }
    }
}